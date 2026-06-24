




using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;


public class ProcessJobService : IProcessJobService
{
    private IProcessJobRepository _processJobRepository;
    public ProcessJobService(IProcessJobRepository processJobRepository)
    {
        _processJobRepository = processJobRepository;
    }

    public async Task<ProcessingJobResponseDto> AddProcessJob(ProcessingJobModel processJob)
    {
        await _processJobRepository.Add(processJob);

        return MapProcessingJobToProcessingResponseDto(processJob);
    }

    public async Task<ProcessingJobResponseDto> GetProcessJobById(int id)
    {
        ProcessingJobModel processingJob = await _processJobRepository.GetById(id);
        
        if(processingJob == null) 
            throw new KeyNotFoundException($"Process Job record with id : {id} was not found");

        return MapProcessingJobToProcessingResponseDto(processingJob);
    }

    private ProcessingJobResponseDto MapProcessingJobToProcessingResponseDto(ProcessingJobModel processingJob)
    {
        ProcessingJobResponseDto processingJobResponseDto = new ()
        {
            Id = processingJob.Id,
            MessageId = processingJob.MessageId,
            CorrelationId = processingJob.CorrelationId,
            SubmissionId = processingJob.SubmissionId,
            FileId = processingJob.FileId,
            Status = processingJob.Status,
            Attempts = processingJob.Attempts,
            ErrorSummary = processingJob.ErrorSummary,
            StartedDate = processingJob.StartedDate,
            CompletedDate = processingJob.CompletedDate
        };

        return processingJobResponseDto;
    }

}