
using Trainee.api.Dto;

namespace Trainee.api.Services;



public interface ILearningTaskService
{

    public Task<List<LearningTaskResponseDto>> GetAllLearningTasks();

    public Task<LearningTaskResponseDto> GetLearningTaskById(int id);

    public Task<LearningTaskResponseDto> AddNewLearningTask(CreateLearningTaskDto createLearningTaskDto);

    public Task<bool> DeleteLearningTaskById(int id);

    public Task<LearningTaskResponseDto> UpdateLearningTaskById(UpdateLearningTaskDto updateLearningTaskDto, int id);

}