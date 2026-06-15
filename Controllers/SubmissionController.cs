using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using Trainee.api.Controllers;
using Trainee.api.Services;
using Trainee.api.dto;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubmissionsController: ControllerBase 
{
    private ISubmissionService _submissionService;

    public SubmissionsController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
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
        SubmissionResponseDto submission = await _submissionService.GetSubmissionById(id);

        if(submission == null)
        {
            return NotFound(new { message = "Submission with id : {id} not found" });
        }

        return Ok(submission);
    }   

    // POST /api/Submissions
    [HttpPost]
    public async Task<ActionResult<SubmissionResponseDto>> CreateSubmission(CreateSubmissionDto createSubmissionDto)
    {
        SubmissionResponseDto submission = await _submissionService.AddSubmission(createSubmissionDto);

        return Ok(submission);
    }
}