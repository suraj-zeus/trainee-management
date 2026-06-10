


using Microsoft.AspNetCore.Mvc;
using Trainee.api.dto;
using Trainee.api.Services;


namespace Trainee.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {


        private AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequestDto)
        {
            
            if(await _authService.IsValidUser(userLoginRequestDto))
            {
                return Ok(new {Token = "token"});
            }

            return Unauthorized();
        }
    }
}