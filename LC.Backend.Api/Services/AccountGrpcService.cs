using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Events.Models;
using LC.Backend.Common.Operations;
using LC.Backend.Common.Services;
using LC.Services.Identity;
using System;
using System.Threading.Tasks;

namespace LC.Backend.Api.Services
{
    public class AccountGrpcService : ServiceBase, IAccountService
    {
        private readonly Identity.IdentityClient _client;

        public AccountGrpcService(Identity.IdentityClient client)
        {
            _client = client;
        }
        public async Task<Result<bool>> CreateAsync(CreateUser user, Guid? correlationId)
        {
            var result = await InvokeWrapedAsync2(() =>
            {
                return Task.FromResult(true);
            } , correlationId);

            return result;
        }

        public async Task<Result<AuthenticateResponse>> LoginAsync(Authenticate authenticate, Guid? correlationId)
        {
            var result = await InvokeWrapedAsync(async () =>
            {
                var login = await _client.LoginAsync(new AuthRequest { Email = authenticate.Email, Password = authenticate.Password });
                return new AuthenticateResponse
                {
                    ErrorMessage = login.ErrorMessage,
                    IsSuccess = login.IsSuccess,
                    Token = new Common.Auth.JsonWebToken
                    {
                        Token = login.Token,
                        Expires = login.Expires
                    }
                };
            }, correlationId);

            return result;
        }
    }
}
