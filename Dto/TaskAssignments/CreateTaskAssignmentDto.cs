
using System.ComponentModel.DataAnnotations;
using Trainee.api.ValidationAttributes;


namespace Trainee.api.Dto;

public class CreateTaskAssignmentDto : IValidatableObject
{

    [Required(ErrorMessage = "TraineeId is required")]
    public int TraineeId { get; set; }

    [Required(ErrorMessage = "MentorId is required")]
    public int MentorId { get; set; }

    [Required(ErrorMessage = "LearningTaskId is required")]
    public int LearningTaskId { get; set; }

    [Required(ErrorMessage = "AssignedDate is required")]
    [DataType(DataType.Date)]
    [DateRangeFromTodayAttribute]
    public DateTime AssignedDate { get; set; }


    [Required(ErrorMessage = "DueDate is required")]
    [DataType(DataType.Date)]
    [DateRangeFromTodayAttribute]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(TaskAssignmentStatus), ErrorMessage = "Invalid Status. Allowed values are:  Assigned, InProgress, Submitted, Reviewed and Completed")]
    public string Status { get; set; }

    public string? Remarks { get; set; }


    // validation for due date and assigned date
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate < AssignedDate)
        {
            yield return new ValidationResult(
                "Due date must be on or after the assigned date.",
                new[] { nameof(DueDate) }
            );
        }
    }
}