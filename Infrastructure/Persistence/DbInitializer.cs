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
            if (_context.Database.IsInMemory())
            {
                await SeedInMemoryDatabaseAsync();
                return;
            }

            if (!await _context.Database.CanConnectAsync())
            {
                return;
            }

            try
            {
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _context.Database.MigrateAsync();
                }

                if (!await _context.ProductTypes.AnyAsync())
                {
                    var typesDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesDate);

                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }


                }

                if (!await _context.ProductsBrands.AnyAsync())
                {
                    var brandsDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductsBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }


                }

                if (!await _context.Products.AnyAsync())
                {
                    var productsDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsDate);
                    if (products is not null && products.Any())
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

        private async Task SeedInMemoryDatabaseAsync()
        {
            if (!await _context.ProductTypes.AnyAsync())
            {
                var typesDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesDate);

                if (types is not null && types.Any())
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }

            if (!await _context.ProductsBrands.AnyAsync())
            {
                var brandsDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);

                if (brands is not null && brands.Any())
                {
                    await _context.ProductsBrands.AddRangeAsync(brands);
                }
            }

            if (!await _context.Products.AnyAsync())
            {
                var productsDate = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsDate);

                if (products is not null && products.Any())
                {
                    await _context.Products.AddRangeAsync(products);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
