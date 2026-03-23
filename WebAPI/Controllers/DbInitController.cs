using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Services;

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
        public async Task Add()
        {
            var dbInit = new DbInit(_context);
            await dbInit.InitialAsync();
        }
    }
}
