using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Models;
using WebAPI.Data;
using WebAPI.Services.IServices;

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
            IEnumerable<CategoryDto> categories = context.Categories.Select(c => new CategoryDto
            {
                CategoryID = c.CategoryID,
                CName = c.CName
            }).ToList();

            return categories;
        }

        public CategoryDto GetById(int id)
        {
            var category = context.Categories.Where(c => c.CategoryID == id).Select(c => new CategoryDto
            {
                CategoryID = c.CategoryID,
                CName = c.CName
            }).FirstOrDefault();

            return category;
        }

        public void Create(CategoryDto categoryDto)
        {
            Category category = new Category()
            {
                CName = categoryDto.CName
            };
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update(int id, CategoryDto categoryDto)
        {
            Category category = context.Categories.Find(id);
            if (category != null)
            {
                category.CName = categoryDto.CName;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Category category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
