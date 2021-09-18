using EAD03.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EAD03
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductService.svc or ProductService.svc.cs at the Solution Explorer and start debugging.
    public class ProductService : IProductService
    {
        private WCFDbContext _context;

        public ProductService()
        {
            this._context = new WCFDbContext();
        }
        public ProductService(WCFDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.AsEnumerable<Product>();
        }

        public void InsertProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
        }
    }
}
