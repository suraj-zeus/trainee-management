

using Trainee.api.Models;

namespace Trainee.api.Repositories;



public interface ISubmissionFileRepository
{
    public Task Add(SubmissionFileModel submissionFile);

    public Task<SubmissionFileModel> FindById(int id);

    public Task Delete(SubmissionFileModel submissionFile);
}