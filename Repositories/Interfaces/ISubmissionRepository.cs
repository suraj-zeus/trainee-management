
using Trainee.api.Models;

namespace Trainee.api.Repositories;

public interface ISubmissionRepository
{
    public Task<List<SubmissionModel>> GetSubmissions();

    public Task<SubmissionModel> GetById(int id);

    public Task Add(SubmissionModel submission);
}