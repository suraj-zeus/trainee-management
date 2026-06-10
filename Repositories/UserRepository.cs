



using Microsoft.EntityFrameworkCore;
using Trainee.api.DatabaseContext;
using Trainee.api.Models;

namespace Trainee.api.Repositories;




public class UserRepository : IUserRepository
{

    private AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<UserModel> GetByUsername(string username)
    {
        UserModel user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

}