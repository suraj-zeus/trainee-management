


using Trainee.api.dto;

namespace Trainee.api.Services;


public interface IAuthService
{
    // public Task<string> LoginService(UserLoginRequestDto userLoginRequestDto);

    public Task<bool> IsValidUser(UserLoginRequestDto userLoginRequestDto);
}