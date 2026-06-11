using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        string searchParamLower = searchParam.ToLower();

        return await _appDbContext
            .Trainees
            .Where(t =>
                t.FirstName.ToLower().Contains(searchParamLower) ||
                t.LastName.ToLower().Contains(searchParamLower) ||
                t.Email.ToLower().Contains(searchParamLower) ||
                t.TechStack.ToLower().Contains(searchParamLower)
            )
            .ToListAsync();
    }

    public async Task<(int,  List<TraineeModel>)> GetPaginatedTrainees(PaginationQueryDto paginationQueryDto)
    {
        string statusParam = paginationQueryDto.Status;
        string searchParamLower = string.IsNullOrEmpty(paginationQueryDto.Search) 
                                    ? "" 
                                    : paginationQueryDto.Search.ToLower();

        var query = _appDbContext.Trainees.AsNoTracking();

        // filter based on search param 
        if(!string.IsNullOrEmpty(searchParamLower)) {
            query = query
                        .Where(t =>
                            t.FirstName.ToLower().Contains(searchParamLower) ||
                            t.LastName.ToLower().Contains(searchParamLower) ||
                            t.Email.ToLower().Contains(searchParamLower) ||
                            t.TechStack.ToLower().Contains(searchParamLower)
                        );
        }

        // filter based on status
        if(!string.IsNullOrEmpty(statusParam)) {
            query = query.Where(t => t.Status == statusParam);
        }

        int totalRecords = await query.CountAsync();

        List<TraineeModel> trainees  = await query
                                            .OrderBy(t => t.Id)
                                            .Skip((paginationQueryDto.PageNumber - 1) * paginationQueryDto.PageSize)
                                            .Take(paginationQueryDto.PageSize)
                                            .ToListAsync();

        return (totalRecords, trainees);
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
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<TraineeModel> UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id)
    {
        TraineeModel trainee = await _appDbContext.Trainees.FindAsync(id);

        if (trainee == null)
            return null;

        trainee.FirstName = updateTraineeDto.FirstName;
        trainee.LastName = updateTraineeDto.LastName;
        trainee.Email = updateTraineeDto.Email;
        trainee.TechStack = updateTraineeDto.TechStack;
        trainee.Status = updateTraineeDto.Status;

        // update updated at timestamp
        trainee.UpdatedAt = DateTime.UtcNow;
        await _appDbContext.SaveChangesAsync();
        return trainee;
    }

}