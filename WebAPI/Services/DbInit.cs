using WebAPI.Data;
using Shared.Models;
using MongoDB.Bson;
using MongoDB.Driver; 

namespace WebAPI.Services
{
    public class DbInit
    {
        private readonly AppDbContext context;
        
        public DbInit(AppDbContext _context)
        {
            context = _context;
        }

        public async Task InitialAsync()
        {
            // Clear existing data
            await context.Products.DeleteManyAsync(FilterDefinition<Product>.Empty);
            await context.Categories.DeleteManyAsync(FilterDefinition<Category>.Empty);

            // Create categories
            var categories = new List<Category>
            {
                new Category { CName = "Electronics" },
                new Category { CName = "Mobile" },
                new Category { CName = "Clothing" }
            };

            await context.Categories.InsertManyAsync(categories);

            // Create products with category references
            var products = new List<Product>
            {
                new Product { PName = "Samsung TV", Price = 314999, Stock = 100, CategoryId = categories[0].Id },
                new Product { PName = "Sony TV", Price = 43490, Stock = 70, CategoryId = categories[0].Id },
                new Product { PName = "iPhone", Price = 99999, Stock = 50, CategoryId = categories[1].Id },
                new Product { PName = "Samsung Galaxy", Price = 89999, Stock = 80, CategoryId = categories[1].Id },
                new Product { PName = "T-Shirt", Price = 2099, Stock = 30, CategoryId = categories[2].Id }
            };

            await context.Products.InsertManyAsync(products);
        }
    }
}
