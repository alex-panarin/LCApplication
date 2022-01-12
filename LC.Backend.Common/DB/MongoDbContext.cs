using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Backend.Common.DB
{
    public class MongoDbContext<TEntity> : IDbContext<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IMongoDatabase _db;
        private readonly Func<IMongoCollection<TEntity>> _collection;

        protected MongoDbContext()
        {

        }
        protected MongoDbContext(IMongoDatabase db, Func<IMongoCollection<TEntity>> collection)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }
        protected IMongoCollection<TEntity> Collection => _collection();

        protected IMongoDatabase Database => _db;

        public async Task<TEntity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> BrowseAsync(Guid? id = null)
            => await Collection
                .AsQueryable()
                .Where(x => id.HasValue ? x.Id == id : true)
                .ToListAsync();

        public async Task AddAsync(TEntity entity)
            => await Collection.InsertOneAsync(entity);

    }
}
