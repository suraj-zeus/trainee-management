


using Trainee.api.Models;

namespace Trainee.api.Repositories;


public interface IProcessJobRepository
{
    public Task Add(ProcessingJobModel processingJob);

    public Task<ProcessingJobModel> GetById(int id);
}