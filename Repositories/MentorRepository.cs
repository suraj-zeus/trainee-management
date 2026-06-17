using Microsoft.EntityFrameworkCore;

using Trainee.api.DatabaseContext;
using Trainee.api.Dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;


public class MentorRepository : IMentorRepository
{
    private AppDbContext _appDbContext;

    public MentorRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<List<MentorModel>> GetMentors()
    {
        return await _appDbContext.Mentors.ToListAsync();
    }

    public async Task<MentorModel> GetById(int id)
    {
        MentorModel mentor = await _appDbContext.Mentors.FindAsync(id);
        return mentor;
    }


    public async Task Add(MentorModel mentor)
    {
        await _appDbContext.Mentors.AddAsync(mentor);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(MentorModel mentor)
    {
        _appDbContext.Mentors.Remove(mentor);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<MentorModel> UpdateMentorById(UpdateMentorDto updateMentorDto, int id)
    {
        MentorModel mentor = await _appDbContext.Mentors.FindAsync(id);

        if (mentor == null)
            return null;

        mentor.FirstName = updateMentorDto.FirstName;
        mentor.LastName = updateMentorDto.LastName;
        mentor.Email = updateMentorDto.Email;
        mentor.Expertise = updateMentorDto.Expertise;
        mentor.Status = updateMentorDto.Status;

        // update updated at timestamp
        mentor.UpdatedDate = DateTime.UtcNow;
        await _appDbContext.SaveChangesAsync();
        return mentor;
    }




}