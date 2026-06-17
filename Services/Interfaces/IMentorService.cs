
using Trainee.api.Dto;

namespace Trainee.api.Services;


public interface IMentorService
{
    public Task<MentorResponseDto> GetMentorById(int id);

    public Task<MentorResponseDto> AddMentor(CreateMentorDto createMentorDto);

    public Task<List<MentorResponseDto>> GetAllMentors();

    public Task<bool> DeleteMentorById(int id);

    public Task<MentorResponseDto> UpdateMentorById(UpdateMentorDto updateMentorDto, int id);

}