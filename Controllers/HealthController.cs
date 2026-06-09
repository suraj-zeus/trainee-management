using Microsoft.AspNetCore.Mvc;


namespace Trainee.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HealthController :
    ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "running",
                application = "Training Management api",
                timestamp = DateTime.UtcNow
            });
        }
    }
}