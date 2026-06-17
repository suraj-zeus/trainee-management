using Trainee.api.ValidationAttributes;
using System.ComponentModel.DataAnnotations;


namespace Trainee.api.Dto;

public class CreateReviewDto
{
    [Required(ErrorMessage = "SubmissionId is required")]
    public int SubmissionId { get; set; }

    [Required(ErrorMessage = "MentorId is required")]
    public int MentorId { get; set; }

    [Required(ErrorMessage = "Feedback is required")]
    public string Feedback { get; set; }

    public string? Score { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(ReviewStatus), ErrorMessage = "Status must be valid: Allowed values are :  Accepted, ChangesRequired and Rejected")]
    public string Status { get; set; }

    [Required(ErrorMessage = "Review Date is required")]
    [DataType(DataType.Date)]
    [DateRangeFromTodayAttribute]
    public DateTime ReviewedDate { get; set; }
}