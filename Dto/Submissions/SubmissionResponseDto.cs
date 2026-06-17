
namespace Trainee.api.Dto;


public class SubmissionResponseDto
{
    public int Id { get; set; }

    public int TaskAssignmentId { get; set; }

    public string SubmissionUrl { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime SubmissionDate { get; set; }

    public String Status { get; set; } = string.Empty;

    // public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
}