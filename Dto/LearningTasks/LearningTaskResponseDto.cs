




namespace Trainee.api.Dto;

public class LearningTaskResponseDto {

    public int Id {get; set;}

    public string Title {get; set;} = string.Empty;

    public string Description {get; set;} = string.Empty;

    public string ExpectedTechStack {get; set;} = string.Empty;

    public DateTime DueDate {get; set;}

    public string Status {get; set;} = string.Empty;

    public DateTime CreatedDate {get; set;}

    public DateTime UpdatedDate {get; set;}

   
}