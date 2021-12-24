using LC.Backend.Common.Commands.Models;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
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
    }
}
