

namespace Trainee.api.Dto;




public class ReviewResponseDto
{
    public int Id { get; set; }

    public required int SubmissionId { get; set; }

    public required int MentorId { get; set; }

    public string Feedback { get; set; }

    public string? Score { get; set; }

    public string Status { get; set; }
    public DateTime ReviewedDate { get; set; }    
   
}