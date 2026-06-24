using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using Trainee.api.Controllers;
using Trainee.api.Services;
using Trainee.api.Dto;
using Trainee.api.Configurations;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private ISubmissionService _submissionService;
    private readonly ILogger<SubmissionsController> _logger;
    ISubmissionFileService _submissionFileService;

    public SubmissionsController(
        ISubmissionService submissionService, 
        ILogger<SubmissionsController> logger,
        ISubmissionFileService submissionFileService
    )
    {
        _submissionService = submissionService;
        _logger = logger;
        _submissionFileService = submissionFileService;
    }

    // GET /api/Submissions
    [HttpGet]
    public async Task<ActionResult<SubmissionResponseDto>> GetAllSubmissions()
    {
        List<SubmissionResponseDto> submissions = await _submissionService.GetAllSubmissions();
        return Ok(submissions);
    }

    // GET /api/Submissions/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SubmissionResponseDto>> GetSubmissionById(int id)
    {
        string requestId = HttpContext.TraceIdentifier;
        SubmissionResponseDto submission = await _submissionService.GetSubmissionById(id);

        if (submission == null)
        {
            _logger.LogInformation($"RequestId : [{requestId}]. The requested submission record with ID : {id} was not found");
            return NotFound(new { message = $"Submission with id : {id} not found" });
        }

        return Ok(submission);
    }

    // POST /api/Submissions
    [HttpPost]
    public async Task<ActionResult<SubmissionResponseDto>> CreateSubmission(CreateSubmissionDto createSubmissionDto)
    {
        string requestId = HttpContext.TraceIdentifier;
        SubmissionResponseDto submission = await _submissionService.AddSubmission(createSubmissionDto);

        _logger.LogInformation($"RequestId : [{requestId}]. The submission record with ID : {submission.Id} created successfully..");
        return Ok(submission);
    }

    // POST /api/submissions/{submissionId}/files 
    [HttpPost("{submissionId}/files")]
    public async Task<ActionResult<UploadSubmissionFileResponseDto>> Upload(int submissionId, CreateSubmissionFileDto createSubmissionFileDto)
    {
        UploadSubmissionFileResponseDto submissionFileResp = await _submissionFileService.Upload(createSubmissionFileDto, submissionId, User);
        return Accepted(submissionFileResp);
    }


   
}