using Shared.Dto;

namespace WebAPI.Services.IServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();

        // GET a product by id
        CategoryDto GetById(string id);

        // Create a new product
        void Create(CategoryDto categoryDto);

        // Update an existing product
        void Update(string id, CategoryDto categoryDto);

        // Delete a product by id
        void Delete(string id);

    }
}
