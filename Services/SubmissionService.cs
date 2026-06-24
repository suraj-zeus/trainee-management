
using System.Security.Claims;
using Trainee.api.Configurations;
using Trainee.api.Constants;
using Trainee.api.Dto;
using Trainee.api.Exceptions;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;

public class SubmissionService : ISubmissionService
{

    private ISubmissionRepository _submissionRepository;
    private ITaskAssignmentRepository _taskAssignmentRepository;
    private ISubmissionFileRepository _submissionFileRepository;
    private IFileStorageService _fileStorageService;
    private IRedisService _redisService;

    public SubmissionService(
        ISubmissionRepository submissionRepository, 
        ITaskAssignmentRepository taskAssignmentRepository,
        IFileStorageService fileStorageService,
        ISubmissionFileRepository submissionFileRepository,
        IRedisService redisService
    )
    {
        _submissionRepository = submissionRepository;
        _taskAssignmentRepository = taskAssignmentRepository;
        _fileStorageService = fileStorageService;
        _submissionFileRepository = submissionFileRepository;
        _redisService = redisService;
    }

    public async Task<List<SubmissionResponseDto>> GetAllSubmissions()
    {
        List<SubmissionModel> submissions = await _submissionRepository.GetSubmissions();
        List<SubmissionResponseDto> submissionsResponse =  new List<SubmissionResponseDto>();

        foreach(SubmissionModel submission in submissions) {
            submissionsResponse.Add(MapSubmissionModelToSubmissionResponseDto(submission));
        }

        return submissionsResponse;
    }


    public async Task<SubmissionResponseDto> GetSubmissionById(int id)
    {
        // first search in cache 
        string redisKey = RedisCacheKeys.Submission(id);
        SubmissionResponseDto submissionResp = await _redisService.GetAsync<SubmissionResponseDto>(redisKey);

        if(submissionResp != null) 
            return submissionResp;

        // if not found in cache, get it from db
        SubmissionModel submission = await _submissionRepository.GetById(id);

        if(submission == null)
            return null;

        submissionResp = MapSubmissionModelToSubmissionResponseDto(submission);

        // add in cache
        await _redisService.SetAsync<SubmissionResponseDto>(redisKey, submissionResp);
        return submissionResp;
    }

    public async Task<SubmissionResponseDto> AddSubmission(CreateSubmissionDto createSubmissionDto)
    {
        TaskAssignmentModel taskAssignment = await _taskAssignmentRepository.GetById(createSubmissionDto.TaskAssignmentId);

        if(taskAssignment == null)
        {
            throw new KeyNotFoundException($"Task Assignment record with id : {createSubmissionDto.TaskAssignmentId} not found");
        }

        SubmissionModel submission = new ()
        {
            TaskAssignmentId = createSubmissionDto.TaskAssignmentId,
            SubmissionUrl = createSubmissionDto.SubmissionUrl,
            Notes = createSubmissionDto.Notes,
            SubmissionDate = createSubmissionDto.SubmissionDate,
            Status = createSubmissionDto.Status
        };

        await _submissionRepository.Add(submission);
        return MapSubmissionModelToSubmissionResponseDto(submission);
    }


    private SubmissionResponseDto MapSubmissionModelToSubmissionResponseDto(SubmissionModel submission)
    {

        SubmissionResponseDto submissionResponseDto = new ()
        {
            Id = submission.Id,
            TaskAssignmentId = submission.TaskAssignmentId,
            SubmissionUrl = submission.SubmissionUrl,
            SubmissionDate = submission.SubmissionDate,
            Notes = submission.Notes,
            Status = submission.Status,
        };

        return submissionResponseDto;
    }


}