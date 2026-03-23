using WebAPI.Data;
using Shared.Dto;
using Shared.Models;
using WebAPI.Services.IServices;
using MongoDB.Driver;

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
            var products = context.Products.Find(_ => true).ToList();
            var categories = context.Categories.Find(_ => true).ToList();
            
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductID = p.Id?.GetHashCode() ?? 0,
                ProductName = p.PName,
                Price = p.Price,
                Stock = p.Stock,
                CategoryID = categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Id?.GetHashCode() ?? 0,
                CategoryObjectId = p.CategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == p.CategoryId)?.CName ?? "Unknown"
            }).ToList();
        }

        public ProductDto GetById(string id)
        {
            var product = context.Products.Find(p => p.Id == id).FirstOrDefault();
            if (product == null)
                return null;

            var category = context.Categories.Find(c => c.Id == product.CategoryId).FirstOrDefault();

            return new ProductDto
            {
                Id = product.Id,
                ProductID = product.Id?.GetHashCode() ?? 0,
                ProductName = product.PName,
                Price = product.Price,
                Stock = product.Stock,
                CategoryID = category?.Id?.GetHashCode() ?? 0,
                CategoryObjectId = product.CategoryId,
                CategoryName = category?.CName ?? "Unknown"
            };
        }

        public void Create(ProductDto productDto)
        {
            var product = new Product
            {
                PName = productDto.ProductName,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryObjectId
            };
            context.Products.InsertOneAsync(product).Wait();
        }

        public void Update(string id, ProductDto productDto)
        {
            var update = Builders<Product>.Update
                .Set(p => p.PName, productDto.ProductName)
                .Set(p => p.Price, productDto.Price)
                .Set(p => p.Stock, productDto.Stock)
                .Set(p => p.CategoryId, productDto.CategoryObjectId);

            context.Products.UpdateOneAsync(p => p.Id == id, update).Wait();
        }

        public void Delete(string id)
        {
            context.Products.DeleteOneAsync(p => p.Id == id).Wait();
        }
    }
}
