using Microsoft.EntityFrameworkCore;
using Trainee.api.DatabaseContext;
using Trainee.api.dto;
using Trainee.api.Models;


namespace Trainee.api.Repositories;

public class TraineeRepository : ITraineeRepository
{

    private AppDbContext _appDbContext;

    public TraineeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<List<TraineeModel>> GetTrainees()
    {
        return await _appDbContext.Trainees.ToListAsync();
    }

    public async Task<List<TraineeModel>> GetTraineesWithSearchParam(string searchParam)
    {
        return await _appDbContext
            .Trainees
            .Where(t =>
                t.FirstName.ToLower().Contains(searchParam.ToLower()) ||
                t.LastName.ToLower().Contains(searchParam.ToLower()) ||
                t.Email.ToLower().Contains(searchParam.ToLower()) ||
                t.TechStack.ToLower().Contains(searchParam.ToLower())
            )
            .ToListAsync();
    }

    public async Task<TraineeModel> GetById(int id)
    {
        TraineeModel trainee = await _appDbContext.Trainees.FindAsync(id);
        return trainee;
    }


    public async Task Add(TraineeModel trainee)
    {
        await _appDbContext.Trainees.AddAsync(trainee);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteById(TraineeModel trainee)
    {
        _appDbContext.Trainees.Remove(trainee);
        _appDbContext.SaveChanges();
    }

    public async Task UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id)
    {
        TraineeModel trainee = await _appDbContext.Trainees.FindAsync(id);

        trainee.FirstName = updateTraineeDto.FirstName;
        trainee.LastName = updateTraineeDto.LastName;
        trainee.Email = updateTraineeDto.Email;
        trainee.TechStack = updateTraineeDto.TechStack;
        trainee.Status = updateTraineeDto.Status;

        // update updated at timestamp
        trainee.UpdatedAt = DateTime.UtcNow;
        await _appDbContext.SaveChangesAsync();
    }

}