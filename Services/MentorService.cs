
using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;

namespace Trainee.api.Services;


public class MentorService : IMentorService
{

    public IMentorRepository _mentorRepository;

    public MentorService(IMentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }



    public async Task<List<MentorResponseDto>> GetAllMentors()
    {
        List<MentorModel> mentors = await _mentorRepository.GetMentors();
        List<MentorResponseDto> mentorsResponse =  new List<MentorResponseDto>();

        foreach(MentorModel mentor in mentors) {
            mentorsResponse.Add(MapMentorModelToMentorResponseDto(mentor));
        }

        return mentorsResponse;
    }


    public async Task<MentorResponseDto> GetMentorById(int id)
    {
        MentorModel mentor = await _mentorRepository.GetById(id);

        if(mentor == null)
            return null;

        return MapMentorModelToMentorResponseDto(mentor);
    }

    public async Task<MentorResponseDto> AddMentor(CreateMentorDto createMentorDto)
    {
        MentorModel mentor = new ()
        {
            FirstName = createMentorDto.FirstName,
            LastName = createMentorDto.LastName,
            Email = createMentorDto.Email,
            Status = createMentorDto.Status,
            Expertise = createMentorDto.Expertise
        };

        // set timestamps
        mentor.CreatedDate = DateTime.UtcNow;
        mentor.UpdatedDate = DateTime.UtcNow;

        await _mentorRepository.Add(mentor);
        return MapMentorModelToMentorResponseDto(mentor);
    }

    public async Task<bool> DeleteMentorById(int id)
    {
        MentorModel mentor = await _mentorRepository.GetById(id);

        if (mentor == null)
            return false;

        await _mentorRepository.Delete(mentor);
        return true;
    }

    public async Task<MentorResponseDto> UpdateMentorById(UpdateMentorDto updateMentorDto, int id)
    {
        MentorModel mentor = await _mentorRepository.UpdateMentorById(updateMentorDto, id);

        if(mentor == null) 
            return null;

        return MapMentorModelToMentorResponseDto(mentor);
    }


    private MentorResponseDto MapMentorModelToMentorResponseDto(MentorModel mentor)
    {
        MentorResponseDto mentorResponseDto = new ()
        {
            Id = mentor.Id,
            FirstName = mentor.FirstName,
            LastName = mentor.LastName,
            Email = mentor.Email,
            Expertise = mentor.Expertise,
            Status = mentor.Status,
            CreatedDate = mentor.CreatedDate,
            UpdatedDate = mentor.UpdatedDate
        };

        return mentorResponseDto;
    }
}