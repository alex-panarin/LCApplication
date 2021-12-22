using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Controllers;
using LC.Services.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase, ISelfTest
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        [HttpGet("/")]
        public async Task<string> Ping()
        {
            return await Task.FromResult($"Ping now: {DateTimeOffset.Now.ToLocalTime()}");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Authenticate authenticate)
        {
            var result = await _userService.LoginAsync(authenticate, Guid.NewGuid());
            if (result.IsSuccess)
                return Ok(result.Data);

            return Unauthorized(result);
        }
    }
}
