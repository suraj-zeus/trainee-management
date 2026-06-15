

namespace Trainee.api.dto;




public class TaskAssignmentResponseDto
{
    public int Id { get; set; } 

    public required int TraineeId { get; set; }

    public required int MentorId { get; set; }

    public required int LearningTaskId { get; set; }

    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }

    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; } = string.Empty;
 
}
