

using Trainee.api.dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;




public interface ITraineeRepository
{
    public Task<List<TraineeModel>> GetTrainees();

    public Task<TraineeModel> GetById(int id);

    public Task Add(TraineeModel trainee);

    public Task DeleteById(TraineeModel traineeModel);

    public Task UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id);

}