using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LC.Backend.Common.DB
{
    public interface IDbContext<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> BrowseAsync(Guid? id);
        Task AddAsync(TEntity entity);
    }
}