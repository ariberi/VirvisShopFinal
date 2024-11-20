using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirvisShopFinal.Models;
using System.Collections.Generic;

namespace VirvisShopFinal.Context
{
    public class VirvisDatabaseContext : DbContext
    {
        public VirvisDatabaseContext(DbContextOptions<VirvisDatabaseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductosDescatados> ProductosDescatados { get; set; }



    }
}