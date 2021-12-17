using LC.Backend.Common.DB;
using LC.Services.Identity.Repositories.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace LC.Services.Identity.Repositories
{
    public class UserDbContext : MongoDbContext<User>
    {
        public UserDbContext(IMongoDatabase db)
            : base(db, ()=> db.GetCollection<User>("user"))
        {
            
        }
        public Task<User> GetAsync(string email)
        {
            return Collection
                .AsQueryable()
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
