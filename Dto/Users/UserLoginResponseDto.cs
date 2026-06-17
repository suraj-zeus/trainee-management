
namespace Trainee.api.Dto;

public class UserLoginResponseDto
{
    
    public string Token {get; set;} = string.Empty;

    public string ExpiresIn {get; set;} = string.Empty;

    public UserResponseDto User {get; set; }
} 