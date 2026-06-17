using System.ComponentModel.DataAnnotations;
using Trainee.api.ValidationAttributes;


namespace Trainee.api.Dto;


public class CreateSubmissionDto
{

    [Required(ErrorMessage = "TaskAssignmentId is required")]
    public int TaskAssignmentId { get; set; }

    [Required(ErrorMessage = "SubmissionUrl is required")]
    public string SubmissionUrl { get; set; }

    [Required(ErrorMessage = "Notes is required")]
    public string Notes { get; set; }

    [Required(ErrorMessage = "SubmissionDate is required")]
    [DataType(DataType.Date)]
    [DateRangeFromTodayAttribute]
    public DateTime SubmissionDate { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(SubmissionStatus), ErrorMessage = "Invalid status! Allowed values are :  Submitted and Resubmitted")]
    public String Status { get; set; } 

}