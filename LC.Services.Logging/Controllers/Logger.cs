using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LC.Services.Logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok("Logger controller");
        }
    }
}
