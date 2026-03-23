using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using Shared.Dto;
using Shared.Models;
using WebAPI.Services;
using WebAPI.Security;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<ProductController>
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

        // GET api/<PRoductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductDto product = productservice.GetById(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        // POST api/<PRoductController>
        [HttpPost]
        public IActionResult Create([FromBody] ProductDto productDto)
        {
            productservice.Create(productDto);
            return CreatedAtAction(nameof(Get), new { id = productDto.ProductID }, productDto);
        }

        // PUT api/<PRoductController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDto productDto)
        {
            productservice.Update(id, productDto);
            return NoContent();
        }

        // DELETE api/<PRoductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productservice.Delete(id);
        }
    }
}
