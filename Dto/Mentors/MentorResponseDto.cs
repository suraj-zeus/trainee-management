


namespace Trainee.api.Dto;

public class MentorResponseDto {
   
    public int Id {get; set;}

    public string FirstName {get; set;} = string.Empty;

    public string LastName {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public string Expertise {get; set;} = string.Empty;

    public string Status {get; set;} = string.Empty;

    public DateTime CreatedDate {get; set;}

    public DateTime UpdatedDate {get; set;}
    
}