

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Trainee.api.Services;
using Trainee.api.dto;


// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReviewsController: ControllerBase 
{
    private IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
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
        ReviewResponseDto review = await _service.GetReviewById(id);
        if(review == null)
        {
            return NotFound(new { message = $"Review with id : {id} not found" });
        }

        return Ok(review);
    }   

    // POST /api/Reviews
    [HttpPost]
    public async Task<ActionResult> CreateReview(CreateReviewDto createReviewDto)
    {
        ReviewResponseDto reviewResponse = await _service.AddReview(createReviewDto);

        return Ok(reviewResponse);
    }
}