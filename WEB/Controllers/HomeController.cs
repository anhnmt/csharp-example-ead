using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WEB.ProductService;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private ProductServiceClient client;

        public HomeController()
        {
            client = new ProductServiceClient();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            var products = client.GetProducts();

            return Json(new
            {
                statusCode = 200,
                message = "Success",
                data = products.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            var errors = new Dictionary<string, string>();

            try
            {
                if (ModelState.IsValid)
                {
                    var check = client.GetProducts().FirstOrDefault(x => x.Name == product.Name);

                    if (check != null)
                    {
                        errors.Add("Name", "Name is duplicated!");

                        return Json(new
                        {
                            statusCode = 400,
                            message = "Error",
                            data = errors
                        }, JsonRequestBehavior.AllowGet);
                    }

                    client.InsertProduct(product);

                    return Json(new
                    {
                        statusCode = 200,
                        message = "Success"
                    }, JsonRequestBehavior.AllowGet);
                }

                foreach (var k in ModelState.Keys)
                    foreach (var err in ModelState[k].Errors)
                    {
                        var key = Regex.Replace(k, @"(\w+)\.(\w+)", @"$2");
                        if (!errors.ContainsKey(key))
                            errors.Add(key, err.ErrorMessage);
                    }

                return Json(new
                {
                    statusCode = 400,
                    message = "Error",
                    data = errors
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    statusCode = 500,
                    message = "Error",
                    data = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}