
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Trainee.api.dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;

public class AuthService : IAuthService
{

    private IUserRepository _userRepository;
    private readonly PasswordHasher<UserModel> _passwordHasher;
    private IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = new();
        _configuration = configuration;
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
        string jwtKey = _configuration["Jwt:Key"];

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
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
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
            ExpiresIn = _configuration["Jwt:ExpiryMinutes"],
            User = userResponseDto
        };

        return userLoginResponseDto;
    }



}