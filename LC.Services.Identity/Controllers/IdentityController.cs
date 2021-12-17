using LC.Backend.Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LC.Services.Identity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase, ISelfTest
    {
        [HttpGet("/")]
        public Task<string> Ping()
        {
            throw new System.NotImplementedException();
        }
    }
}
