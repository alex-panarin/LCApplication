using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Events;
using LC.Backend.Common.Events.Models;
using LC.Services.Identity.Services;
using RawRabbit.Extensions.Client;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Handlers
{
    public class AuthenticateHandler : IEventHandler<AuthenticateRequest>
    {
        private readonly IUserService _service;
        private readonly IBusClient _client;

        public AuthenticateHandler(IUserService service,
            IBusClient client)
        {
            _service = service;
            _client = client;
        }
        public async Task HandleAsync(AuthenticateRequest @event, Guid globalMessageId)
        {
            //var correlationId = Guid.NewGuid();
            var result = await _service.LoginAsync(new Authenticate { Email = @event.email, Password = @event.password }, globalMessageId);

            await _client.PublishAsync(new AuthenticateResponse { Token = result.Data, ErrorMessage = result.ErrorMessages, IsSuccess = result.IsSuccess }, globalMessageId);
        }
    }
}
