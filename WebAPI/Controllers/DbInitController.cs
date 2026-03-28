using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    // A Http híváshoz kötött útvonalat definiálja és a DbInit
    // szolgáltatást használja a műveletek végrehajtásához.

    [Route("api/[controller]")]
    [ApiController]
    public class DbInitController : ControllerBase
    {
        private readonly AppDbContext context;

        public DbInitController(AppDbContext _context)
        {
            context = _context;
        }
        
        [HttpPost("add")]
        public async Task Add()
        {
            var dbInit = new DbInit(context);
            await dbInit.InitialAsync();
        }
    }
}
