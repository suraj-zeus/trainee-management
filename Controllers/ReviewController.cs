

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Trainee.api.Services;
using Trainee.api.Dto;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReviewsController: ControllerBase 
{
    private IReviewService _service;
     private readonly ILogger<ReviewsController> _logger;

    public ReviewsController(IReviewService service, ILogger<ReviewsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET /api/Reviews
    [HttpGet]
    public async Task<ActionResult<List<ReviewResponseDto>>> GetAllReviews()
    {
        List<ReviewResponseDto> reviews = await _service.GetAllReviews();
        return Ok(reviews);
    }

    // GET /api/Reviews/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewResponseDto>> GetReviewById(int id)
    {
        string requestId = HttpContext.TraceIdentifier;
        ReviewResponseDto review = await _service.GetReviewById(id);
        if(review == null)
        {
            _logger.LogInformation($"RequestId : [{requestId}]. The requested review record with ID : {id} was not found");
            return NotFound(new { message = $"Review with id : {id} not found" });
        }

        return Ok(review);
    }   

    // POST /api/Reviews
    [HttpPost]
    public async Task<ActionResult> CreateReview(CreateReviewDto createReviewDto)
    {
        string requestId = HttpContext.TraceIdentifier;
        ReviewResponseDto reviewResponse = await _service.AddReview(createReviewDto);

        _logger.LogInformation($"RequestId : [{requestId}]. The review record with ID : {reviewResponse.Id} created successfully..");
        return Ok(reviewResponse);
    }
}