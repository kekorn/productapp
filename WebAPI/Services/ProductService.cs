using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using Shared.Dto;
using Shared.Models;
using WebAPI.Services.IServices;

namespace WebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;
        public ProductService(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<ProductDto> GetAll()
        {
            //List<ProductDto> products = new();
            //foreach (var item in context.Products)
            //{
            //    products.Add(new ProductDto
            //    {
            //        ProductID = item.ProductID,
            //        ProductName = item.PName,
            //        Price = item.Price,
            //        Stock = item.Stock,
            //        CategoryName = item.Category.CName
            //    });
            //}

            // Egyszerusített megadás lambda függvénnyel:
            // A Select() metódus segítségével minden Product objektumot egy új ProductDto objektummá alakítunk át,
            // majd a ToList() metódussal egy listává alakítjuk az eredményt.
            IEnumerable<ProductDto> products = context.Products.Select(p => new ProductDto
            {
                ProductID = p.ProductID,
                ProductName = p.PName,
                Price = p.Price,
                CategoryID = p.CategoryID,
                CategoryName = p.Category.CName
            }).ToList();

            return products;
        }

        public ProductDto? GetById(int id)
        {
            Product? product = context.Products.Find(id);
            if (product != null)
            {
                ProductDto productDto = new ProductDto()
                {
                    ProductID = product.ProductID,
                    ProductName = product.PName,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryID = product.CategoryID,
                    CategoryName = product.Category.CName
                };
                return productDto;
            }
            return null;
        }

        public void Create(ProductDto productDto)
        {
            Product product = new Product()
            {
                PName = productDto.ProductName,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryID = productDto.CategoryID
            };
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(int id, ProductDto productDto)
        {

            Product product = context.Products.Find(id);
            if (product != null)
            {
                product.PName = productDto.ProductName;
                product.Price = productDto.Price;
                product.Stock = productDto.Stock;
                product.CategoryID = productDto.CategoryID;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
