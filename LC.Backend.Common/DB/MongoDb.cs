using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Backend.Common.DB
{
    public class MongoDb : IDbInitializer
    {
        private readonly IMongoDatabase _database;
        private readonly string[] _collections;
        public MongoDb(IMongoDatabase database, IDbTables tables)
        {
            _database = database;
            _collections = tables.Tables;
        }
        public async Task InitializeAsync()
        {
            var existCollections = await _database.ListCollectionNamesAsync();
            var collectionsToCreate = _collections.Except(await existCollections.ToListAsync());

            foreach (var collection in collectionsToCreate)
            {
                await _database.CreateCollectionAsync(collection);
            }
        }
    }
}
