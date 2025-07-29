using Microsoft.AspNetCore.Mvc;

namespace IM.Content.Controllers
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
            return Ok("IM.Content.Api is Working ....");
        }
    }
}
