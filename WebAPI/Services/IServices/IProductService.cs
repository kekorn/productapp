using Shared.Dto;
using Shared.Models;

namespace WebAPI.Services.IServices
{
    /// <summary>
    /// We can achive the abstraction by creating an interface for the service layer, 
    /// and then implementing that interface in a concrete class. This allows us to decouple the service 
    /// layer from the rest of the application, making it easier to test and maintain.
    /// 
    /// Az absztrakciót úgy érhetjük el, hogy létrehozunk egy interfészt a service réteg számára, majd ezt 
    /// az interfészt egy konkrét osztályban implementáljuk. Ez lehetové teszi, hogy a service réteg független 
    /// legyen az alkalmazás többi részétol, így könnyebbé válik a tesztelése és karbantartása.
    /// </summary>
    public interface IProductService
    {
        // Az IEnumerable egy kollekciót (elemek sorozatát) jelképez, amelyen végig lehet iterálni (pl. foreach).
        // Ez a legáltalánosabb gyujtemény-típus C#-ban. Olyan lista, amirol „egymás után kérek elemeket”.
        // Az IQueryable<T> egy olyan gyujtemény, amely támogatja a lekérdezések végrehajtását egy adatforráson
        // (pl. adatbázis) keresztül. Ez lehetové teszi a hatékony lekérdezést és az adatok szurését, rendezését
        // stb. közvetlenül az adatforrásban.
        // Az IEnumerable általában memóriában lévo gyujteményekre használatos, míg az IQueryable adatforrásokra, 
        // amelyek támogatják a lekérdezések végrehajtását.
        // A ketto közötti választás attól függ, hogy milyen muveleteket szeretnénk végrehajtani a gyujteményen,
        // és hogy az adatforrás támogatja-e a lekérdezéseket.
        IEnumerable<ProductDto> GetAll();

        ProductDto GetById(string id);

        void Create(ProductDto productDto);

        void Update(string id, ProductDto productDto);

        void Delete(string id);
    }
}
