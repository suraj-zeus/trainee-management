
using Trainee.api.Dto;

namespace Trainee.api.Services;


public interface ITraineeService
{
    public Task<List<TraineeResponseDto>> GetAllTraineesWithSeachParam(string searchParam);

    public Task<List<TraineeResponseDto>> GetAllTrainees();

    public  Task<PaginationResponseDto<TraineeResponseDto>> GetPaginatedTrainees(PaginationQueryDto paginationQueryDto);

    public Task<TraineeResponseDto> GetTraineeById(int id);

    public Task<TraineeResponseDto> AddTrainee(CreateTraineeDto createTraineeDto);

    public Task<bool> DeleteTraineeById(int id);

    public Task<TraineeResponseDto> UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id);



}