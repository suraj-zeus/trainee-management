
using Trainee.api.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Trainee.api.Models;
using Trainee.api.Configurations;

namespace Trainee.api.Services;

public class DbSeederService : IDbSeederService
{

    private AppDbContext _appDbContext;
    private AdminDefaultUserConfig _adminDefaultUserConfig;

    public DbSeederService(AppDbContext appDbContext,  IOptions<AdminDefaultUserConfig> options)
    {
        _appDbContext = appDbContext;
        _adminDefaultUserConfig = options.Value;
    }

    public async Task SeedAdminUserAsync()
    {

        if (await _appDbContext.Users.AnyAsync(u => u.Username == _adminDefaultUserConfig.Username))
        {
            return;
        }
  
        var adminUser = new UserModel
        {
            Username = _adminDefaultUserConfig.Username,
            Email = _adminDefaultUserConfig.Email,
            PasswordHash = "",
            Role = UserRole.Admin.ToString(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
 
        var hasher = new PasswordHasher<UserModel>();
        string hashedPassword = hasher.HashPassword(adminUser, _adminDefaultUserConfig.Password);
        adminUser.PasswordHash = hashedPassword;
 
        await _appDbContext.Users.AddAsync(adminUser);
        await _appDbContext.SaveChangesAsync();
    }
}