






using Trainee.api.Dto;
using Trainee.api.Models;

namespace Trainee.api.Services;


public interface IProcessJobService
{
    public Task<ProcessingJobResponseDto> AddProcessJob(ProcessingJobModel processJob);

    public Task<ProcessingJobResponseDto> GetProcessJobById(int id);


}