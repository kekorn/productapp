using MongoDB.Driver;
using Shared.Models;
using System.Linq;
using WebAPI.Data;

namespace WebAPI.Security
{
    /// <summary>
    /// DEPRECATED: This class is no longer used. Authentication is handled by AuthAPI using JWT tokens.
    /// This class remains for backward compatibility only.
    /// </summary>
    [System.Obsolete("Use JWT token authentication from AuthAPI instead")]
    public class APISecurity
    {
        private readonly AppDbContext _context;

        public APISecurity(AppDbContext context)
        {
            _context = context;
        }

        public bool Validate(string username, string password)
        {
            // No longer validates against User collection
            // All authentication is handled by AuthAPI
            return false;
        }
    }
}
