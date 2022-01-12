using LC.Services.Identity.Repositories.Entities;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> GetAsync(Guid id)
           => await _dbContext.GetAsync(id);

        public async Task<User> GetAsync(string email)
            => await _dbContext.GetAsync(email);


        public async Task AddAsync(User user)
            => await _dbContext.AddAsync(user);
    }
}
