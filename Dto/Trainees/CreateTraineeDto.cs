
using System.ComponentModel.DataAnnotations;

namespace Trainee.api.Dto;





public class CreateTraineeDto {


    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    public string FirstName {get; set;} = string.Empty;


    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    public string LastName {get; set;} = string.Empty;


    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email {get; set;} = string.Empty;


    [Required(ErrorMessage = "Techstack is required")]
    public string TechStack {get; set;} = string.Empty;


    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(TraineeStatus), ErrorMessage = "Invalid Status. Allowed values are: Active, Inactive, and Completed")]
    public string Status {get; set;} = string.Empty;


}