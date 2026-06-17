

using System.ComponentModel.DataAnnotations;
using Trainee.api.ValidationAttributes;

namespace Trainee.api.Dto;


public class CreateLearningTaskDto {

    [Required(ErrorMessage = "Title is required")]
    public string Title {get; set;} = string.Empty;

    [Required(ErrorMessage = "Description   is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 50 characters")]
    public string Description {get; set;} = string.Empty;

    [Required(ErrorMessage = "ExpectedTechStack  is required")]
    public string ExpectedTechStack  {get; set;} = string.Empty;

    [Required(ErrorMessage = "DueDate  is required")]
    [DataType(DataType.Date)]
    [DateRangeFromTodayAttribute]
    public DateTime DueDate {get; set;}

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(LearningTaskStatus), ErrorMessage = "Invalid Status. Allowed values are: Draft, Published and Closed")]
    public string Status {get; set;} = string.Empty;
}