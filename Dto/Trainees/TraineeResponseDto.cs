


namespace Trainee.api.dto;

public class TraineeResponseDto {
    public int Id {get; set;}

    public string FirstName {get; set;} = string.Empty;

    public string LastName {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public string TechStack {get; set;} = string.Empty;

    public string Status {get; set;} = string.Empty;

    public DateTime CreatedAt {get; set;}

    public DateTime UpdatedAt {get; set;}
}