using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace AuthAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // A Lazy loading használatához telepíteni kell a Microsoft.EntityFrameworkCore.Proxies csomagot, és engedélyezni kell a lazy loading proxy-k használatát
        // az OnConfiguring metódusban az AppDbContext osztályban.
        // UseLazyLoadingProxies() metódus használata a lazy loading engedélyezéséhez az AppDbContext osztályban.
        // A kapcsolódó entitásoknak virtual kulcsszóval kell rendelkezniük a modellünk navigációs tulajdonságokban, hogy a lazy loading muködjön. (Foreign Key)

        public DbSet<MyUser> MyUsers { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.\\sqlexpress;Database=ProductApp;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
        //}
    }
}
