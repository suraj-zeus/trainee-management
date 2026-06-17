using Microsoft.EntityFrameworkCore;

using Trainee.api.DatabaseContext;
using Trainee.api.Dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;


public class LearningTaskRepository : ILearningTaskRepository
{

    private AppDbContext _appDbContext;

    public LearningTaskRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<List<LearningTaskModel>> GetLearningTasks()
    {
        return await _appDbContext.LearningTasks.ToListAsync();
    }


    public async Task<LearningTaskModel> GetById(int id)
    {
        LearningTaskModel learningTask = await _appDbContext.LearningTasks.FindAsync(id);
        return learningTask;
    }


    public async Task Add(LearningTaskModel learningTask)
    {
        await _appDbContext.LearningTasks.AddAsync(learningTask);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(LearningTaskModel learningTask)
    {
        _appDbContext.LearningTasks.Remove(learningTask);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<LearningTaskModel> UpdateLearningTaskById(UpdateLearningTaskDto updateLearningTaskDto, int id)
    {
        LearningTaskModel learningTask = await _appDbContext.LearningTasks.FindAsync(id);

        if (learningTask == null)
            return null;

        learningTask.Title = updateLearningTaskDto.Title;
        learningTask.Description = updateLearningTaskDto.Description;
        learningTask.Status = updateLearningTaskDto.Status;
        learningTask.DueDate = updateLearningTaskDto.DueDate;
        learningTask.ExpectedTechStack = updateLearningTaskDto.ExpectedTechStack;


        // update updated at timestamp
        learningTask.UpdatedDate = DateTime.UtcNow;
        await _appDbContext.SaveChangesAsync();
        return learningTask;
    }
    
}