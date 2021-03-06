using LC.Backend.Api.Services;
using LC.Backend.Common.Commands.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LC.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(IAccountService));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
        {
            var result = await _service.CreateAsync(user, Guid.NewGuid());
            return Accepted(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Authenticate authenticate)
        {
            var result = await _service.LoginAsync(authenticate, Guid.NewGuid());

            return !result.IsSuccess
                ? BadRequest(result.ErrorMessages)
                : result.Data?.IsSuccess == true
                ? Ok(result.Data.Token)
                : Unauthorized(result.Data.ErrorMessage);

        }
    }
}
