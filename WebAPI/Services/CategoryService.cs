using Shared.Dto;
using Shared.Models;
using WebAPI.Data;
using WebAPI.Services.IServices;
using MongoDB.Driver;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;
        
        public CategoryService(AppDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = context.Categories.Find(_ => true).ToList();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                CategoryID = c.Id?.GetHashCode() ?? 0,
                CName = c.CName
            }).ToList();
        }

        public CategoryDto GetById(string id)
        {
            var category = context.Categories.Find(c => c.Id == id).FirstOrDefault();
            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                CategoryID = category.Id?.GetHashCode() ?? 0,
                CName = category.CName
            };
        }

        public void Create(CategoryDto categoryDto)
        {
            var category = new Category
            {
                CName = categoryDto.CName
            };
            context.Categories.InsertOneAsync(category).Wait();
        }

        public void Update(string id, CategoryDto categoryDto)
        {
            var update = Builders<Category>.Update.Set(c => c.CName, categoryDto.CName);
            context.Categories.UpdateOneAsync(c => c.Id == id, update).Wait();
        }

        public void Delete(string id)
        {
            context.Categories.DeleteOneAsync(c => c.Id == id).Wait();
        }
    }
}
