using MongoDB.Driver;
using Shared.Models;

namespace AuthAPI.Data

{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IMongoClient mongoClient, string databaseName = "ProductApp")
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public async Task InitializeAsync()
        {
            // Táblák létrehozása, ha még nem léteznek
            try
            {
                var collections = await _database.ListCollectionNamesAsync();
                var collectionNames = await collections.ToListAsync();

                if (!collectionNames.Contains("Categories"))
                {
                    await _database.CreateCollectionAsync("Categories");
                }
                if (!collectionNames.Contains("Products"))
                {
                    await _database.CreateCollectionAsync("Products");
                }
            }
            catch
            {
                // Táblák létrehozása nem szükséges, ha már léteznek, így a kivétel elhanyagolható
            }
        }

        public void SaveChanges()
        {
            // MongoDB mentése automatikusan történik, így ez a metódus üres maradhat
        }
    }
}
