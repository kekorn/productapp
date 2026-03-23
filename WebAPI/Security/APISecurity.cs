using MongoDB.Driver;
using Shared.Models;
using System.Linq;
using WebAPI.Data;
namespace WebAPI.Security
{
    public class APISecurity
    {
        private readonly AppDbContext _context;

        public APISecurity(AppDbContext context)
        {
            _context = context;
        }

        public bool Validate(string username, string password)
        {
            try
            {
                MyUser user = _context.MyUsers.AsQueryable()
                    .SingleOrDefault(u => u.Username == username && u.Password == password);
                return user != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
