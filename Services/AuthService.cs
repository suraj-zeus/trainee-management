
using Microsoft.AspNetCore.Identity;


using Trainee.api.dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;

public class AuthService : IAuthService
{


    private IUserRepository _userRepository;
    private readonly PasswordHasher<UserModel> _passwordHasher;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new();
    }

    public async Task<bool> IsValidUser(UserLoginRequestDto userLoginRequestDto)
    {
        UserModel user = await _userRepository.GetByUsername(userLoginRequestDto.Username);

        if(user == null) 
            return false;

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword( 
            user, 
            user.PasswordHash, 
            userLoginRequestDto.Password
        );

        if(result == PasswordVerificationResult.Failed)
            return false;

        return true;
    }


    // public Task<string> LoginService(UserLoginRequestDto userLoginRequestDto)
    // {
    //     return "token";
    // }
}