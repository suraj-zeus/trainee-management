using System.ComponentModel.DataAnnotations;

namespace Trainee.api.Models;

public class TaskAssignmentModel
{
    public int Id { get; set; } 

    public int TraineeId { get; set; }
    public TraineeModel Trainee { get; set; } = null!;

    public int MentorId { get; set; }
    public MentorModel Mentor { get; set; } = null!;

    public int LearningTaskId { get; set; }
    public LearningTaskModel LearningTask { get; set; } = null!; 

    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }

    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; } = string.Empty;


     public ICollection<SubmissionModel> Submissions { get; set; } = new List<SubmissionModel>();
 
}
