using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Models;
using WebAPI.Data;
using WebAPI.Services;
using WebAPI.Services.IServices;


namespace WebAPI.Controllers
{
    // A Http hívásokhoz kötött útvonalakat definiálja és a CategoryService
    // szolgáltatást használja a mûveletek végrehajtásához.

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {        
        private readonly AppDbContext context;
        private readonly CategoryService categoryService;
        
        public CategoryController(AppDbContext _context)
        {
            context = _context;
            categoryService = new CategoryService(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CategoryDto> categories = categoryService.GetAll();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            CategoryDto category = categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto categoryDto)
        {
            categoryService.Create(categoryDto);
            return CreatedAtAction(nameof(Get), categoryDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] CategoryDto categoryDto)
        {
            categoryService.Update(id, categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            categoryService.Delete(id);
        }
    }
}
