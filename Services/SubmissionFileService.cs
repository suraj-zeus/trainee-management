using System.Security.Claims;

using Trainee.api.Dto;
using Trainee.api.Exceptions;
using Trainee.api.Messaging;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;



public class SubmissionFileService : ISubmissionFileService
{
    private ISubmissionFileRepository _submissionFileRepository;
    private IFileStorageService _fileStorageService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ISubmissionRepository _submissionRepository;
    private IRabbitMqService _rabbitMqService;
    
    public SubmissionFileService(
        ISubmissionRepository submissionRepository,
        ISubmissionFileRepository submissionFileRepository,
        IFileStorageService fileStorageService,
        IRabbitMqService rabbitMqService,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _submissionRepository = submissionRepository;
        _fileStorageService = fileStorageService;
        _submissionFileRepository = submissionFileRepository;
        _rabbitMqService = rabbitMqService;
        _httpContextAccessor = httpContextAccessor;
    }


    
    public async Task<UploadSubmissionFileResponseDto> Upload(CreateSubmissionFileDto createSubmissionFileDto, int submissionId, ClaimsPrincipal claimsPrincipalUser)
    {
        IFormFile formFile = createSubmissionFileDto.formFile;

        SubmissionModel submission = await _submissionRepository.GetById(submissionId);

        if(submission == null)
            throw new KeyNotFoundException($"Submission record with ID : {submissionId} was not found");

        bool isValid = _fileStorageService.validate(formFile);

        if(!isValid)
        {
            throw new BadRequestException("Invalid file request");
        }

        var fileExt = Path.GetExtension(formFile.FileName).ToLowerInvariant();
        var storageName = $"{Guid.NewGuid()}{fileExt}";

        await using var stream = formFile.OpenReadStream();
        var checkSum = await _fileStorageService.ComputeCheckSum(stream);

        await _fileStorageService.SaveAsync(stream, storageName);

        int userId = int.Parse(claimsPrincipalUser.FindFirstValue(ClaimTypes.NameIdentifier)!);

        SubmissionFileModel submissionFile = new ()
        {
            SubmissionId = submissionId,
            OriginalFileName = Path.GetFileName(formFile.FileName),
            StorageName = storageName,
            ContentType = formFile.ContentType,
            FileSizeBytes = formFile.Length,
            CheckSum = checkSum,
            UploadedByUserId = userId,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        await _submissionFileRepository.Add(submissionFile);


        // publish message in rabbitmq 
        string correlationId = _httpContextAccessor.HttpContext.TraceIdentifier;

        SubmissionProcessingRequest submissionProcessingRequest = new()
        {
            MessageId = Guid.NewGuid().ToString(),
            CorrelationId = correlationId,
            SubmissionId = submissionFile.SubmissionId,
            FileId = submissionFile.Id,
            RequestedAt = DateTime.UtcNow,
            ContractVersion = 1
        };

        await _rabbitMqService.PublishAsync(submissionProcessingRequest);

        return MapSubmissionFileToUploadSubmissionFileResponseDto(submissionFile, correlationId);
    }



    public async Task<(Stream, string, string)> DownloadFile(int id, ClaimsPrincipal claimsPrincipalUser)
    {
        int userId = int.Parse(claimsPrincipalUser.FindFirstValue(ClaimTypes.NameIdentifier)!);

        SubmissionFileModel submissionFile = await _submissionFileRepository.FindById(id);

        if(submissionFile == null)
        {
            throw new KeyNotFoundException($"Submission File record with id : {id} was not found");
        }

        // authorisation check
        if(submissionFile.UploadedByUserId != userId)
        {
            throw new ForbiddenException($"You do not have permission to read/modify this file.");
        }

        if(!await _fileStorageService.ExistsAsync(submissionFile.StorageName))
        {
            throw new KeyNotFoundException($"Physical file is mission for the given file id : {id}");
        }

        Stream stream = await _fileStorageService.OpenReadAsync(submissionFile.StorageName);

        return (stream, submissionFile.ContentType, submissionFile.OriginalFileName);
    }


    
    public async Task<bool> DeleteFile(int id, ClaimsPrincipal claimsPrincipalUser)
    {
        int userId = int.Parse(claimsPrincipalUser.FindFirstValue(ClaimTypes.NameIdentifier)!);

        SubmissionFileModel submissionFile = await _submissionFileRepository.FindById(id);

        if(submissionFile == null)
        {
            throw new KeyNotFoundException($"Submission File record with id : {id} was not found");
        }

        // authorisation check
        if(submissionFile.UploadedByUserId != userId)
        {
            throw new ForbiddenException($"You do not have permission to read/modify this file.");
        }

        if(!await _fileStorageService.ExistsAsync(submissionFile.StorageName))
        {
            throw new KeyNotFoundException($"Physical file is mission for the given file id : {id}");
        }

        await _fileStorageService.DeleteAsync(submissionFile.StorageName);

        // delete from db
        await _submissionFileRepository.Delete(submissionFile);

        return true;
    }


    private UploadSubmissionFileResponseDto MapSubmissionFileToUploadSubmissionFileResponseDto(SubmissionFileModel submissionFile, string correlationId)
    {
        UploadSubmissionFileResponseDto uploadSubmissionFile = new ()
        {
            Id = submissionFile.Id,
            SubmissionId = submissionFile.SubmissionId,
            OriginalFileName = submissionFile.OriginalFileName,
            StorageName = submissionFile.StorageName,
            ContentType = submissionFile.ContentType,
            FileSizeBytes = submissionFile.FileSizeBytes,
            CheckSum = submissionFile.CheckSum,
            UploadedByUserId = submissionFile.UploadedByUserId,
            CreatedDate = submissionFile.CreatedDate,
            UpdatedDate = submissionFile.UpdatedDate,
            CorrelationId = correlationId
        };

        return uploadSubmissionFile;
    }





    
}