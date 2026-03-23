using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbInitController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DbInitController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("add")]
        public void Add()
        {
        var dbInit = new DbInit(_context);
            dbInit.Initial();
        }
    }
}
