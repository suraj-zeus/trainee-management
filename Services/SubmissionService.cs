


using Trainee.api.dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;

public class SubmissionService : ISubmissionService
{

    private ISubmissionRepository _submissionRepository;
    private ITaskAssignmentRepository _taskAssignmentRepository;

    public SubmissionService(ISubmissionRepository submissionRepository, ITaskAssignmentRepository taskAssignmentRepository)
    {
        _submissionRepository = submissionRepository;
        _taskAssignmentRepository = taskAssignmentRepository;
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
        SubmissionModel submission = await _submissionRepository.GetById(id);

        if(submission == null)
            return null;

        return MapSubmissionModelToSubmissionResponseDto(submission);
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