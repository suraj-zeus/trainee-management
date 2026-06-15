


public class SubmissionResponseDto
{
    public int Id { get; set; }

    public int TaskAssignmentId { get; set; }

    public string SubmissionUrl { get; set; }

    public string Notes { get; set; }

    public DateTime SubmissionDate { get; set; }

    public String Status { get; set; }

    // public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
}