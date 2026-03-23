using Shared.Models;
using System.Linq;
using WebAPI.Data;
namespace WebAPI.Security
{
    public class APISecurity
    {
        public static bool Validate(string username, string password)
        {
            AppDbContext context = new AppDbContext();
            try
            {
                MyUser user = context.MyUsers.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    return true;
                }
            }
            catch (Exception ex) { 
                return false;
            }
            return false;
        }
    }
}
