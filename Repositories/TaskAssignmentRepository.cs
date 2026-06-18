using Microsoft.EntityFrameworkCore;



using Trainee.api.DatabaseContext;
using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Repositories;

public class TaskAssignmentRepository : ITaskAssignmentRepository
{
    private AppDbContext _appDbContext;

    public TaskAssignmentRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    
    public async Task<List<TaskAssignmentModel>> GetTaskAssignments()
    {
        return await _appDbContext
            .TaskAssignments
            // .Include(t => t.Submissions)
            .ToListAsync();
    }

    public async Task<TaskAssignmentModel> GetById(int id)
    {
        TaskAssignmentModel taskAssignment = await _appDbContext
                                                        .TaskAssignments
                                                        // .Include(t => t.Submissions)
                                                        .FindAsync(id);
        return taskAssignment;
    }


    public async Task Add(TaskAssignmentModel taskAssignment)
    {
        await _appDbContext.TaskAssignments.AddAsync(taskAssignment);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(TaskAssignmentModel taskAssignment)
    {
        _appDbContext.TaskAssignments.Remove(taskAssignment);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<TaskAssignmentModel> UpdateTaskAssignmentById(UpdateTaskAssignmentDto updateTaskAssignmentDto, int id)
    {
        TaskAssignmentModel taskAssignment = await _appDbContext.TaskAssignments.FindAsync(id);

        if(taskAssignment == null)
            return null;

        taskAssignment.Status = updateTaskAssignmentDto.Status;
        await _appDbContext.SaveChangesAsync();
        return taskAssignment;
    }




}