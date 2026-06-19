

using System.ComponentModel.DataAnnotations;



namespace Trainee.api.Dto;

public class UserLoginRequestDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username {get; set;} = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password {get; set;} = string.Empty;


}