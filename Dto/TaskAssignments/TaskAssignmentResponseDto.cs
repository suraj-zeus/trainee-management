


namespace Trainee.api.Dto;




public class TaskAssignmentResponseDto
{
    public int Id { get; set; } 

    public int TraineeId { get; set; }

    public int MentorId { get; set; }

    public int LearningTaskId { get; set; }

    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }

    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; } = string.Empty;

    // public List<SubmissionResponseDto> Submissions { get; set; } = new List<SubmissionResponseDto>();
 
}
