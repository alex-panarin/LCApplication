using LC.Services.Identity.Repositories.Entities;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
    }
}
