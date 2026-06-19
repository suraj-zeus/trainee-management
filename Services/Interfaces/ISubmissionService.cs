using System.Security.Claims;


using Trainee.api.Dto;
using Trainee.api.Models;

namespace Trainee.api.Services;

public interface ISubmissionService
{
    
    public Task<List<SubmissionResponseDto>> GetAllSubmissions();

    public Task<SubmissionResponseDto> GetSubmissionById(int id);
    public Task<SubmissionResponseDto> AddSubmission(CreateSubmissionDto createSubmissionDto);


}