using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LC.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiController : ControllerBase
    {
        public ApiController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTask()
        {
            return await Task.FromResult(Ok("GetTask"));
        }
    }
}
