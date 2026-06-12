


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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequestDto)
        {
            
            UserLoginResponseDto userLoginResponseDto = await _authService.LoginService(userLoginRequestDto);

            if(userLoginResponseDto == null)
            {
                _logger.LogError($"Login attempt failed for user with username : {userLoginRequestDto.Username}");
                return BadRequest();
            }

            _logger.LogInformation($"User with username : {userLoginRequestDto.Username} logged in successfully!");
            return Ok(userLoginResponseDto);
        }
    }
}