using MongoDB.Driver;
using Shared.Models;

namespace WebAPI.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IMongoClient mongoClient, string databaseName = "ProductApp")
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

        public async Task InitializeAsync()
        {
            // Create collections if they don't exist
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
                // Collections might already exist
            }
        }

        public void SaveChanges()
        {
            // MongoDB automatically saves changes
        }
    }
}
