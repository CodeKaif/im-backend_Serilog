using Microsoft.AspNetCore.Mvc;

namespace IM.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("IM.Auth.Api is Working ..");
        }
    }
}
