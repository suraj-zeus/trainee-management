
using Trainee.api.Dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;


public interface ILearningTaskRepository
{
    public Task<List<LearningTaskModel>> GetLearningTasks();

    public Task<LearningTaskModel> GetById(int id);

    public Task Add(LearningTaskModel learningTask);

    public Task Delete(LearningTaskModel learningTask);

    public Task<LearningTaskModel> UpdateLearningTaskById(UpdateLearningTaskDto updateLearningTaskDto, int id);
}