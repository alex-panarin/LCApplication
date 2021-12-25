using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Events.Models;
using Microsoft.AspNetCore.Mvc;
using RawRabbit.Extensions.Client;
using RawRabbit.Extensions.MessageSequence;
using System;
using System.Threading.Tasks;

namespace LC.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBusClient _busClient;

        public AccountController(IBusClient busClient)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
        {
            await _busClient.PublishAsync(user);
            return Accepted();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Authenticate authenticate)
        {
            var authMessage = new AuthenticateRequest { email = authenticate.Email, password = authenticate.Password };
            var sequence = _busClient.ExecuteSequence(c => c
               .PublishAsync(authMessage)
               .Complete<AuthenticateResponse>()) ;

            var result = await sequence.Task.ConfigureAwait(true);

            return result.IsSuccess 
                ? Ok(result.Token) 
                : Unauthorized(result.ErrorMessage);
        }
    }
}
