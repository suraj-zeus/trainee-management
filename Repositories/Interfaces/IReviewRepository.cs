


using Trainee.api.Models;

namespace Trainee.api.Repositories;

public interface IReviewRepository
{
    public Task<List<ReviewModel>> GetReviews();

    public Task<ReviewModel> GetById(int id);

    public Task Add(ReviewModel review);
}