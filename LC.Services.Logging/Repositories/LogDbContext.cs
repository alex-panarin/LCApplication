using LC.Backend.Common.DB;
using LC.Services.Logging.Entities;
using MongoDB.Driver;

namespace LC.Services.Logging.Repositories
{
    public class LogDbContext : MongoDbContext<LogRow>
    {
        public LogDbContext(IMongoDatabase database)
            : base(database, () => database.GetCollection<LogRow>("logs"))
        {

        }
    }
}
