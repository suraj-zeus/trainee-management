


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using Trainee.api.Controllers;
using Trainee.api.Services;
using Trainee.api.Dto;
using Trainee.api.Configurations;


[Authorize]
[ApiController]
[Route("api/submission-files")]
public class SubmissionFilesController : ControllerBase
{
    private ISubmissionFileService _submissionFileService;
    private readonly ILogger<SubmissionFilesController> _logger;

    public SubmissionFilesController(ISubmissionFileService submissionFileService, ILogger<SubmissionFilesController> logger)
    {
        _submissionFileService = submissionFileService;
        _logger = logger;
    }

   
    //  GET /api/submission-files/{id}/download
    [HttpGet("{id}/download")]
    public async Task<ActionResult> Download(int id)
    {
        var (stream, contentType, originalName) = await _submissionFileService.DownloadFile(id, User);

        if (stream == null)
        {
            return NotFound(new {Message = $"Submission file record with id : {id} was not found"});
        }

        return File(stream, contentType, originalName);
    }


    //  DELETE /api/submission-files/{id} 
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool isDeleted = await _submissionFileService.DeleteFile(id, User);

        if (!isDeleted)
        {
            return NotFound(new {Message = $"Submission file record with id : {id} was not found"});
        }

        return Ok(new {Message = $"Submission file record with ID : {id} deleted successfully"});
    }
}