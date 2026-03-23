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
            // Create collections if they don't exist
            try
            {
                var collections = await _database.ListCollectionNamesAsync();
                var collectionNames = await collections.ToListAsync();

                if (!collectionNames.Contains("Users"))
                {
                    await _database.CreateCollectionAsync("Users");
                }
            }
            catch
            {
                // Collection might already exist
            }
        }

        public void SaveChanges()
        {
            // MongoDB automatically saves changes
        }
    }
}
