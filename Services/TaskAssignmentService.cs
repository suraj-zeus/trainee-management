



using Trainee.api.Constants;
using Trainee.api.Controllers;
using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;
using Trainee.api.Services;

namespace Trainee.api.Services;

public class TaskAssignmentService : ITaskAssignmentService
{

    private ITaskAssignmentRepository _taskAssignmentRepository;
    private IMentorRepository _mentorRepository;
    private ITraineeRepository _traineeRepository;
    private ILearningTaskRepository _learningTaskRepository;
    private IRedisService _redisService;


    public TaskAssignmentService(
        ITaskAssignmentRepository taskAssignmentRepository,
        IMentorRepository mentorRepository,
        ITraineeRepository traineeRepository,
        ILearningTaskRepository learningTaskRepository,
        IRedisService redisService
    )
    {
        _taskAssignmentRepository = taskAssignmentRepository;
        _learningTaskRepository = learningTaskRepository;
        _traineeRepository = traineeRepository;
        _mentorRepository = mentorRepository;
        _redisService = redisService;
    }

    public async Task<List<TaskAssignmentResponseDto>> GetAllTaskAssignments()
    {
        List<TaskAssignmentModel> assignments = await _taskAssignmentRepository.GetTaskAssignments();
        List<TaskAssignmentResponseDto> assigmentsResp = new List<TaskAssignmentResponseDto>();

        foreach (TaskAssignmentModel assignment in assignments)
        {
            assigmentsResp.Add(MapTaskAssignmentToTaskAssignmentResponseDto(assignment));
        }

        return assigmentsResp;
    }


    public async Task<TaskAssignmentResponseDto> CreateTaskAssignment(CreateTaskAssignmentDto createTaskAssignmentDto)
    {

        TraineeModel trainee = await _traineeRepository.GetById(createTaskAssignmentDto.TraineeId);
        MentorModel mentor = await _mentorRepository.GetById(createTaskAssignmentDto.MentorId);
        LearningTaskModel learningTask = await _learningTaskRepository.GetById(createTaskAssignmentDto.LearningTaskId);

        var errors = new List<string>();

        if (trainee == null)
            errors.Add($"Trainee with ID {createTaskAssignmentDto.TraineeId} not found.");

        if (mentor == null)
            errors.Add($"Mentor with ID {createTaskAssignmentDto.MentorId} not found.");

        if (learningTask == null)
            errors.Add($"Learning Task with ID {createTaskAssignmentDto.LearningTaskId} not found.");

        if (errors.Any())
        {
            throw new KeyNotFoundException(string.Join(Environment.NewLine, errors));
        }

        TaskAssignmentModel taskAssignment = new()
        {
            TraineeId = createTaskAssignmentDto.TraineeId,
            MentorId = createTaskAssignmentDto.MentorId,
            LearningTaskId = createTaskAssignmentDto.LearningTaskId,
            AssignedDate = createTaskAssignmentDto.AssignedDate,
            DueDate = createTaskAssignmentDto.DueDate,
            Status = createTaskAssignmentDto.Status,
            Remarks = createTaskAssignmentDto.Remarks
        };

        await _taskAssignmentRepository.Add(taskAssignment);
        return MapTaskAssignmentToTaskAssignmentResponseDto(taskAssignment);
    }



    public async Task<TaskAssignmentResponseDto> GetTaskAssignmentById(int id)
    {
        // first search in cache 
        string redisKey = RedisCacheKeys.TaskAssignment(id);
        TaskAssignmentResponseDto taskAssignResp = await _redisService.GetAsync<TaskAssignmentResponseDto>(redisKey);

        if(taskAssignResp != null) return taskAssignResp;

        // if not found in cache, search in db
        TaskAssignmentModel taskAssignment = await _taskAssignmentRepository.GetById(id);

        if(taskAssignment == null) {
            return null;
        }

        taskAssignResp = MapTaskAssignmentToTaskAssignmentResponseDto(taskAssignment);
        
        // add in cache
        await _redisService.SetAsync<TaskAssignmentResponseDto>(redisKey, taskAssignResp);
        return taskAssignResp;
    }



    public async Task<TaskAssignmentResponseDto> UpdateTaskAssignmentDetails(int id, UpdateTaskAssignmentDto updateTaskAssignmentDto)
    {
        // delete the record from cache
        string redisKey = RedisCacheKeys.TaskAssignment(id);
        await _redisService.RemoveAsync(redisKey);

        TaskAssignmentModel taskAssignment = await _taskAssignmentRepository.UpdateTaskAssignmentById(updateTaskAssignmentDto, id);

        if(taskAssignment == null)
        {
            throw new KeyNotFoundException("Task assignment record with id : {id} not found");
        }

        return MapTaskAssignmentToTaskAssignmentResponseDto(taskAssignment);
    }




    private TaskAssignmentResponseDto MapTaskAssignmentToTaskAssignmentResponseDto(TaskAssignmentModel assignment)
    {
        TaskAssignmentResponseDto taskAssignmentResponse = new()
        {
            Id = assignment.Id,
            TraineeId = assignment.LearningTaskId,
            MentorId = assignment.MentorId,
            LearningTaskId = assignment.LearningTaskId,
            AssignedDate = assignment.AssignedDate,
            DueDate = assignment.DueDate,
            Status = assignment.Status,
            Remarks = assignment.Remarks,
            // Submissions = assignment.Submissions.Select(s => new SubmissionResponseDto
            // {
            //     Id = s.Id,
            //     TaskAssignmentId = s.TaskAssignmentId,
            //     SubmissionUrl = s.SubmissionUrl,
            //     Notes = s.Notes,
            //     SubmissionDate = s.SubmissionDate,
            //     Status = s.Status
            // }).ToList()
        };

        return taskAssignmentResponse;
    }
}