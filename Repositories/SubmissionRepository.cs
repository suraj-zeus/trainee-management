

using Microsoft.EntityFrameworkCore;

using Trainee.api.DatabaseContext;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Repositories;


public class SubmissionRepository : ISubmissionRepository
{

    private AppDbContext _appDbContext;

    public SubmissionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<List<SubmissionModel>> GetSubmissions()
    {
        return await _appDbContext.Submissions.ToListAsync();
    }

    public async Task<SubmissionModel> GetById(int id)
    {
        SubmissionModel submission = await _appDbContext.Submissions.FindAsync(id);
        return submission;
    }


    public async Task Add(SubmissionModel submission)
    {
        await _appDbContext.Submissions.AddAsync(submission);
        await _appDbContext.SaveChangesAsync();
    }

}