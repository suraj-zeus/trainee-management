


namespace Trainee.api.Configurations;

public class AdminDefaultUserConfig
{
    public const string SectionName = "AdminUser";
    public string Username {get; set; }= string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}