using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Models;
using WebAPI.Data;
using WebAPI.Security;
using WebAPI.Services;
using WebAPI.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
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
        // GET: api/<CategoryController>
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

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CategoryDto category = categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }
        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto categoryDto)
        {
            categoryService.Create(categoryDto);
            return CreatedAtAction(nameof(Get), new { id = categoryDto.CategoryID }, categoryDto);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryDto categoryDto)
        {
            categoryService.Update(id, categoryDto);
            return NoContent();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            categoryService.Delete(id);
        }
    }
}
