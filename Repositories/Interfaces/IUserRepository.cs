



using Trainee.api.Models;

namespace Trainee.api.Repositories;




public interface IUserRepository
{

    public Task<UserModel> GetByUsername(string username);

}