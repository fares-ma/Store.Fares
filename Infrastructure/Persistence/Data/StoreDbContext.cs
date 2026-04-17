using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    // CLR
    public class StoreDbContext : DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }





        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductsBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);


            base.OnModelCreating(modelBuilder);

            
        }



    }
}
