

using System.ComponentModel.DataAnnotations;



namespace Trainee.api.Dto;

public class UserLoginRequestDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username {get; set;} = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password length must be between 2 and 50 characters")]
    public string Password {get; set;} = string.Empty;


}