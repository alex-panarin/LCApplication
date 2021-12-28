using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Events.Models;
using LC.Backend.Common.Operations;
using System;
using System.Threading.Tasks;

namespace LC.Backend.Api.Services
{
    public interface IAccountService
    {
        Task<Result<bool>> CreateAsync(CreateUser user, Guid? correlationId);
        Task<Result<AuthenticateResponse>>  LoginAsync(Authenticate authenticate, Guid? correlationId);
    }
}
