


using Microsoft.AspNetCore.Mvc;
using Trainee.api.Dto;
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
            string requestId = HttpContext.TraceIdentifier;
            UserLoginResponseDto userLoginResponseDto = await _authService.LoginService(userLoginRequestDto);

            if(userLoginResponseDto == null)
            {
                _logger.LogError($"RequestId : [{requestId}]. Login attempt failed for user with username : {userLoginRequestDto.Username}");
                return BadRequest(new {Message = "Invalid Credentials.."});
            }

            _logger.LogInformation($"RequestId : [{requestId}]. User with username : {userLoginRequestDto.Username} logged in successfully!");
            return Ok(userLoginResponseDto);
        }
    }
}