

using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;


public class ReviewService : IReviewService
{
    private IReviewRepository _reviewRepository;
    private IMentorRepository _mentorRepository;
    private ISubmissionRepository _submissionRepository; 

    public ReviewService(
        IReviewRepository reviewRepository,
        IMentorRepository mentorRepository,
        ISubmissionRepository submissionRepository
    )
    {
        _reviewRepository = reviewRepository;
        _mentorRepository = mentorRepository;
        _submissionRepository = submissionRepository;
    }
    

    
    public async Task<List<ReviewResponseDto>> GetAllReviews()
    {
        List<ReviewModel> reviews = await _reviewRepository.GetReviews();
        List<ReviewResponseDto> reviewsResponse =  new List<ReviewResponseDto>();

        foreach(ReviewModel review in reviews) {
            reviewsResponse.Add(MapReviewToReviewResposeDto(review));
        }

        return reviewsResponse;
    }


    public async Task<ReviewResponseDto> GetReviewById(int id)
    {
        ReviewModel review = await _reviewRepository.GetById(id);

        if(review == null)
            return null;

        return MapReviewToReviewResposeDto(review);
    }

    public async Task<ReviewResponseDto> AddReview(CreateReviewDto createReviewDto)
    {
        MentorModel mentor = await _mentorRepository.GetById(createReviewDto.MentorId);
        SubmissionModel submission = await _submissionRepository.GetById(createReviewDto.SubmissionId);

        var errors = new List<string>();

        if (mentor == null)
            errors.Add($"Mentor with ID : {createReviewDto.MentorId} not found.");

        if (submission == null)
            errors.Add($"Submission record with ID : {createReviewDto.SubmissionId} not found.");

        if (errors.Any())
        {
            throw new KeyNotFoundException(string.Join(Environment.NewLine, errors));
        }

        ReviewModel review = new ()
        {
            SubmissionId = createReviewDto.SubmissionId,
            MentorId = createReviewDto.MentorId,
            Feedback = createReviewDto.Feedback,
            Score = createReviewDto.Score,
            Status = createReviewDto.Status,
            ReviewedDate = createReviewDto.ReviewedDate
        };

        await _reviewRepository.Add(review);
        return MapReviewToReviewResposeDto(review);
    }


    private ReviewResponseDto MapReviewToReviewResposeDto(ReviewModel review)
    {
        ReviewResponseDto reviewResponseDto = new ()
        {
            Id = review.Id,
            SubmissionId = review.SubmissionId,
            MentorId = review.MentorId,
            Feedback = review.Feedback,
            Score = review.Score,
            Status = review.Status,
            ReviewedDate = review.ReviewedDate
        };

        return reviewResponseDto;
    }



}