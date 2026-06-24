




namespace Trainee.api.Configurations;

public class FileStorageConfig
{
    public const string SectionName = "FileStorage";
    public string RootPath {get; set; }= string.Empty;
    public long MaxFileSizeBytes { get; set; }
    public List<string> AllowedExtensions {get; set;}= new();

    public List<string> AllowedContentTypes {get; set;} = new();
}