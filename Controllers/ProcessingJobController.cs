using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Trainee.api.Dto;
using Trainee.api.Services;





[Authorize]
[ApiController]
[Route("api/processing-jobs")]
public class ProcessingJobController : ControllerBase
{
    private readonly ILogger<ProcessingJobController> _logger;
    private IProcessJobService _processJobService;

    public ProcessingJobController(
        ILogger<ProcessingJobController> logger,
        IProcessJobService processJobService
    )
    {
        _processJobService = processJobService;
        _logger = logger;
    }

   

    // GET /api/processing-jobs/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcessingJobResponseDto>> GetProcessJobById(int id)
    {
        ProcessingJobResponseDto processingJobResponse = await _processJobService.GetProcessJobById(id);

        return Ok(processingJobResponse);
    }



}