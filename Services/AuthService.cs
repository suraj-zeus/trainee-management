
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;

using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;
using Trainee.api.Configurations;

namespace Trainee.api.Services;

public class AuthService : IAuthService
{

    private IUserRepository _userRepository;
    private readonly PasswordHasher<UserModel> _passwordHasher;
    private JwtConfig _jwtConfig;

    public AuthService(
        IUserRepository userRepository, 
        IOptions<JwtConfig> options
    )
    {
        _userRepository = userRepository;
        _passwordHasher = new();
        _jwtConfig = options.Value;
    }



    // handle login request
    public async Task<UserLoginResponseDto> LoginService(UserLoginRequestDto userLoginRequestDto)
    {
        UserModel user = await _userRepository.GetByUsername(userLoginRequestDto.Username);

        if (user == null)
            return null;

        bool isUserValid = VerifyUserPassword(user, userLoginRequestDto.Password);

        if(!isUserValid)
            return null;

        string token = GenerateJwtToken(user);

        return MapToUserLoginResponseDto(user, token);
    }


    private bool VerifyUserPassword(UserModel user, string enteredPassword)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            enteredPassword
        );

        if (result == PasswordVerificationResult.Failed)
            return false;

        return true;
    }

    // generate json web token
    private string GenerateJwtToken(UserModel user)
    {
        // generate jwt token
        string jwtKey = _jwtConfig.Key;

        if(jwtKey == null)
            return null;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes((double) _jwtConfig.ExpiryMinutes!),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = credentials
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);
        return token;
    }



    //  map login response data to required response type
    private UserLoginResponseDto MapToUserLoginResponseDto(UserModel user, string token)
    {

        UserResponseDto userResponseDto = new()
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
        };

        UserLoginResponseDto userLoginResponseDto = new ()
        {
            Token = token,
            ExpiresIn = _jwtConfig.ExpiryMinutes.ToString(),
            User = userResponseDto
        };

        return userLoginResponseDto;
    }



}