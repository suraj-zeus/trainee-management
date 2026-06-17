


using Trainee.api.Dto;

namespace Trainee.api.Services;


public interface IAuthService
{
    public Task<UserLoginResponseDto> LoginService(UserLoginRequestDto userLoginRequestDto);

}