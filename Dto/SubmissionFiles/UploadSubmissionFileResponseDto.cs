

namespace Trainee.api.Dto;


public class UploadSubmissionFileResponseDto
{
     public int Id { get; set; }

    public int SubmissionId {get; set; }


    public string OriginalFileName {get; set; } = string.Empty;
    
    public string StorageName {get; set; } = string.Empty;

    public string ContentType {get; set; } = string.Empty;

    public long FileSizeBytes {get; set; } 

    public string CheckSum {get; set;} = string.Empty;

    public int UploadedByUserId {get; set; } 

    public DateTime CreatedDate {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedDate {get; set; } = DateTime.UtcNow;

    public string CorrelationId {get; set;} = string.Empty;

}