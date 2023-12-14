using Microsoft.AspNetCore.Mvc;

namespace Onyx.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CheckHealth()
        {
            _logger.LogInformation("Health check endpoint called");
            return Ok("Healthy Api");
        }
    }
}
