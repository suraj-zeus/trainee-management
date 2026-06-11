


using Microsoft.AspNetCore.Mvc;
using Trainee.api.dto;
using Trainee.api.Services;


namespace Trainee.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {


        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequestDto)
        {
            
            UserLoginResponseDto userLoginResponseDto = await _authService.LoginService(userLoginRequestDto);

            if(userLoginResponseDto == null)
            {
                return BadRequest();
            }

            return Ok(userLoginResponseDto);
        }
    }
}