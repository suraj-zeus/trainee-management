
using Trainee.api.Dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;


public interface ITaskAssignmentRepository
{
    
    public Task<List<TaskAssignmentModel>> GetTaskAssignments();
    public Task<TaskAssignmentModel> GetById(int id);

    public Task Add(TaskAssignmentModel taskAssignment);

    public Task Delete(TaskAssignmentModel taskAssignment);

    public Task<TaskAssignmentModel> UpdateTaskAssignmentById(UpdateTaskAssignmentDto updateTaskAssignmentDto, int id);
}