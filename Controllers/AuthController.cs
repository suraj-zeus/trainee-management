


using Microsoft.AspNetCore.Mvc;
using Trainee.api.dto;


namespace Trainee.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(UserLoginRequestDto userLoginRequestDto)
        {

            
            return Ok();
        }
    }
}