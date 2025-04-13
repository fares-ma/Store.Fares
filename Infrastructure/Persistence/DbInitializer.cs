using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext Context)
        {
            _context = Context;
        }

        public async Task InitializeAsync()
        {

            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                if (_context.productTypes.Any())
                {
                    var typesDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesDate);

                    if (types is not null & types.Any())
                    {
                        await _context.productTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }


                }

                if (_context.productBrands.Any())
                {
                    var brandsDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);
                    if (brands is not null & brands.Any())
                    {
                        await _context.productBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }


                }

                if (_context.Products.Any())
                {
                    var productsDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsDate);
                    if (products is not null & products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }



        }    
    }
}
