

using Trainee.api.DatabaseContext;
using Trainee.api.Models;

namespace Trainee.api.Repositories;


public class SubmissionFileRepository : ISubmissionFileRepository
{

    private AppDbContext _appDbContext;

    public SubmissionFileRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(SubmissionFileModel submissionFile)
    {
        await _appDbContext.SubmissionFiles.AddAsync(submissionFile);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<SubmissionFileModel> FindById(int id)
    {
        return await _appDbContext.SubmissionFiles.FindAsync(id);
    }

    public async Task Delete(SubmissionFileModel submissionFile)
    {
        _appDbContext.SubmissionFiles.Remove(submissionFile);
        await _appDbContext.SaveChangesAsync();
    }
}