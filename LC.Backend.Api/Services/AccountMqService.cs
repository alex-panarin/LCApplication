using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Events.Models;
using LC.Backend.Common.Operations;
using LC.Backend.Common.Services;
using RawRabbit.Extensions.Client;
using RawRabbit.Extensions.MessageSequence;
using System;
using System.Threading.Tasks;

namespace LC.Backend.Api.Services
{
    public class AccountMqService :
        ServiceBase,
        IAccountService
    {
        private readonly IBusClient _client;

        public AccountMqService(IBusClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(IBusClient));
        }
        public async Task<Result<bool>> CreateAsync(CreateUser user, Guid? correlationId)
        {
            var result = await InvokeWrapedAsync2(() => _client.PublishAsync(user), correlationId);

            return result;
        }

        public async Task<Result<AuthenticateResponse>> LoginAsync(Authenticate authenticate, Guid? correlationId)
        {
            var result = await InvokeWrapedAsync(() =>
            {
                var authMessage = new AuthenticateRequest { email = authenticate.Email, password = authenticate.Password };
                var sequence = _client.ExecuteSequence(c => c
                .PublishAsync(authMessage)
                .Complete<AuthenticateResponse>());

                return sequence.Task;
            }, correlationId);

            return result;
        }
    }
}
