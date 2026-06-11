

using Trainee.api.dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;




public interface ITraineeRepository
{
    public Task<List<TraineeModel>> GetTrainees();

    public Task<List<TraineeModel>> GetTraineesWithSearchParam(string searchParam);

    public Task<(int,  List<TraineeModel>)> GetPaginatedTrainees(PaginationQueryDto paginationQueryDto);

    public Task<TraineeModel> GetById(int id);

    public Task Add(TraineeModel trainee);

    public Task DeleteById(TraineeModel traineeModel);

    public Task<TraineeModel> UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id);

}