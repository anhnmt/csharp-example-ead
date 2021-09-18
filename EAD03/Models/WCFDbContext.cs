using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EAD03.Models
{
    public class WCFDbContext : DbContext
    {
        public WCFDbContext() : base("name=WCFConnectionString")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}