using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class MobileStoreContext : DbContext
    {
        public MobileStoreContext() : base ("DefaultConnection")
        {
                
        }
        public DbSet<tblroles> tblroles { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<CartOrder> orders { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartOrderDetail> orderDetails { get; set; }





    }
}