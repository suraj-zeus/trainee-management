


using Trainee.api.dto;

namespace Trainee.api.Services;


public interface IAuthService
{
    public Task<UserLoginResponseDto> LoginService(UserLoginRequestDto userLoginRequestDto);

}