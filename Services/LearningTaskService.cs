
using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;


public class LearningTaskService : ILearningTaskService
{
    public ILearningTaskRepository _learningTaskRepository;

    public LearningTaskService(ILearningTaskRepository learningTaskRepository)
    {
        _learningTaskRepository = learningTaskRepository;
    }



    public async Task<List<LearningTaskResponseDto>> GetAllLearningTasks()
    {
        List<LearningTaskModel> learningTasks = await _learningTaskRepository.GetLearningTasks();
        List<LearningTaskResponseDto> learningTasksResponse =  new List<LearningTaskResponseDto>();

        foreach(LearningTaskModel learningTask in learningTasks) {
            learningTasksResponse.Add(MapLearningTaskToLearningTaskResponseDto(learningTask));
        }

        return learningTasksResponse;
    }


    public async Task<LearningTaskResponseDto> GetLearningTaskById(int id)
    {
        LearningTaskModel learningTask = await _learningTaskRepository.GetById(id);

        if(learningTask == null)
            return null;

        return MapLearningTaskToLearningTaskResponseDto(learningTask);
    }

    public async Task<LearningTaskResponseDto> AddNewLearningTask(CreateLearningTaskDto createLearningTaskDto)
    {
        LearningTaskModel learningTask = new ()
        {
            Title = createLearningTaskDto.Title,
            Description = createLearningTaskDto.Description,
            DueDate = createLearningTaskDto.DueDate,
            Status = createLearningTaskDto.Status,
            ExpectedTechStack = createLearningTaskDto.ExpectedTechStack
        };

        // set timestamps
        learningTask.CreatedDate = DateTime.UtcNow;
        learningTask.UpdatedDate = DateTime.UtcNow;

        await _learningTaskRepository.Add(learningTask);
        return MapLearningTaskToLearningTaskResponseDto(learningTask);
    }

    public async Task<bool> DeleteLearningTaskById(int id)
    {
        LearningTaskModel learningTask = await _learningTaskRepository.GetById(id);

        if (learningTask == null)
            return false;

        await _learningTaskRepository.Delete(learningTask);
        return true;
    }

    public async Task<LearningTaskResponseDto> UpdateLearningTaskById(UpdateLearningTaskDto updateLearningTaskDto, int id)
    {
        LearningTaskModel learningTask = await _learningTaskRepository.UpdateLearningTaskById(updateLearningTaskDto, id);

        if(learningTask == null) 
            return null;

        return MapLearningTaskToLearningTaskResponseDto(learningTask);
    }


    private LearningTaskResponseDto MapLearningTaskToLearningTaskResponseDto(LearningTaskModel learningTask)
    {
        LearningTaskResponseDto learningTaskResponse = new ()
        {
            Id = learningTask.Id,
            Title = learningTask.Title,
            Description = learningTask.Description,
            Status = learningTask.Status,
            ExpectedTechStack = learningTask.ExpectedTechStack,
            DueDate = learningTask.DueDate,
            CreatedDate = learningTask.CreatedDate,
            UpdatedDate = learningTask.UpdatedDate
        };

        return learningTaskResponse;
    }
}