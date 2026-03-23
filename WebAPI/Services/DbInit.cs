using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using Shared.Models;

namespace WebAPI.Services
{
    public class DbInit
    {
        private readonly AppDbContext context;
        public DbInit(AppDbContext _context)
        {
            context = _context;
        }

        public void Initial()
        {
            context.Products.RemoveRange(context.Products);
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();

            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Categories', RESEED, 0)");

            List<Category> categories = new List<Category>
            {
                new Category { CName = "Electronics" },
                new Category { CName = "Mobile" },
                new Category { CName = "Clothing" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            List<Product> products = new List<Product>
            {
                new Product { PName = "Samsung TV", Price = 314999, Stock = 100, CategoryID = categories[0].CategoryID },
                new Product { PName = "Sony TV", Price = 43490, Stock = 70, CategoryID = categories[0].CategoryID },
                new Product { PName = "iPhone", Price = 99999, Stock = 50, CategoryID = categories[1].CategoryID },
                new Product { PName = "Samsung Galaxy", Price = 89999, Stock = 80, CategoryID = categories[1].CategoryID },
                new Product { PName = "T-Shirt", Price = 2099, Stock = 30, CategoryID = categories[2].CategoryID }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            //List<MyUser> users = new List<MyUser>
            //{
            //    new MyUser { Username = "admin", Password = "12345" },
            //    new MyUser { Username = "scott",Password="tiger" }
            //};
            //context.MyUsers.AddRange(users);
            //context.SaveChanges();
        }
    }
}
