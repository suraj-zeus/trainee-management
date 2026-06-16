
using Trainee.api.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trainee.api.Models;

namespace Trainee.api.Services;

public class DbSeederService : IDbSeederService
{

    private AppDbContext _appDbContext;

    public DbSeederService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SeedAdminUserAsync()
    {
        if (await _appDbContext.Users.AnyAsync(u => u.Username == "admin"))
        {
            return;
        }
  
        var adminUser = new UserModel
        {
            Username = "admin",
            Email = "admin@gmail.com",
            PasswordHash = "",
            Role = UserRole.Admin.ToString(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
 
        var hasher = new PasswordHasher<UserModel>();
        string hashedPassword = hasher.HashPassword(adminUser, "admin@123");
        adminUser.PasswordHash = hashedPassword;
 
        await _appDbContext.Users.AddAsync(adminUser);
        await _appDbContext.SaveChangesAsync();
    }
}