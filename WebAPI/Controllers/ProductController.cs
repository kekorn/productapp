using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using Shared.Dto;
using Shared.Models;
using WebAPI.Services;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ProductService productservice;

        public ProductController(AppDbContext _context)
        {
            context = _context;
            productservice = new ProductService(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ProductDto> products = productservice.GetAll();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            ProductDto product = productservice.GetById(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDto productDto)
        {
            productservice.Create(productDto);
            return CreatedAtAction(nameof(Get), productDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] ProductDto productDto)
        {
            productservice.Update(id, productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            productservice.Delete(id);
        }
    }
}
