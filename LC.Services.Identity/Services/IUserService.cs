using LC.Backend.Common.Auth;
using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Operations;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Services
{
    public interface IUserService
    {
        Task<Result<bool>> CreateAsync(string email, string name, string pass, Guid correlationId);
        Task<Result<JsonWebToken>> LoginAsync(Authenticate authenticate, Guid correlationId);
    }
}
