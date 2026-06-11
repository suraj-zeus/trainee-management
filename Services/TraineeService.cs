
using Trainee.api.dto;
using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;


namespace Trainee.api.Services;

public class TraineeService : ITraineeService
{

    private ITraineeRepository _traineeRepository;

    public TraineeService(ITraineeRepository traineeRepository)
    {
        _traineeRepository = traineeRepository;
    }

    public async Task<List<TraineeResponseDto>> GetAllTrainees()
    {
        List<TraineeModel> trainees = await _traineeRepository.GetTrainees();
        List<TraineeResponseDto> traineesResponse =  new List<TraineeResponseDto>();

        foreach(TraineeModel trainee in trainees) {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return traineesResponse;
    }

    public async Task<PaginationResponseDto<TraineeResponseDto>> GetPaginatedTrainees(PaginationQueryDto paginationQueryDto)
    {
        var (totalRecords, trainees) = await _traineeRepository.GetPaginatedTrainees(paginationQueryDto);
        List<TraineeResponseDto> traineesResponse =  new List<TraineeResponseDto>();

        foreach(TraineeModel trainee in trainees) {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return MapToPaginatedTraineeResponse(traineesResponse, paginationQueryDto, totalRecords);
    }

    public async Task<List<TraineeResponseDto>> GetAllTraineesWithSeachParam(string searchParam)
    {
        List<TraineeModel> trainees = await _traineeRepository.GetTraineesWithSearchParam(searchParam);
        List<TraineeResponseDto> traineesResponse =  new List<TraineeResponseDto>();

        foreach(TraineeModel trainee in trainees) {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return traineesResponse;
    }

    public async Task<TraineeResponseDto> GetTraineeById(int id)
    {
        TraineeModel trainee = await _traineeRepository.GetById(id);

        if(trainee == null)
            return null;
        return MapTraineeModelToTraineeResponseDto(trainee);
    }

    public async Task<TraineeResponseDto> AddTrainee(CreateTraineeDto createTraineeDto)
    {
        TraineeModel trainee = new ()
        {
            FirstName = createTraineeDto.FirstName,
            LastName = createTraineeDto.LastName,
            Email = createTraineeDto.Email,
            TechStack = createTraineeDto.TechStack,
            Status = createTraineeDto.Status
        };

        // set timestamps
        trainee.CreatedAt = DateTime.UtcNow;
        trainee.UpdatedAt = DateTime.UtcNow;

        await _traineeRepository.Add(trainee);
        return MapTraineeModelToTraineeResponseDto(trainee);
    }


    public async Task<bool> DeleteTraineeById(int id)
    {
        TraineeModel trainee = await _traineeRepository.GetById(id);

        if (trainee == null)
            return false;

        await _traineeRepository.DeleteById(trainee);
        return true;
    }

    public async Task<TraineeResponseDto> UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id)
    {
        TraineeModel trainee = await _traineeRepository.UpdateTraineeById(updateTraineeDto, id);

        if(trainee == null) 
            return null;

        return MapTraineeModelToTraineeResponseDto(trainee);
    }


    private PaginationResponseDto<TraineeResponseDto> MapToPaginatedTraineeResponse(List<TraineeResponseDto> traineesResponse, PaginationQueryDto paginationQueryDto, int totalRecords)
    {
        PaginationResponseDto<TraineeResponseDto> paginatedResponse = new ()
        {
            PageNumber = paginationQueryDto.PageNumber,
            PageSize = paginationQueryDto.PageSize,
            TotalRecords = totalRecords,
            Data = traineesResponse
        };

        return paginatedResponse;
    }


    private TraineeResponseDto MapTraineeModelToTraineeResponseDto(TraineeModel traineeModel) {

        TraineeResponseDto traineeResponseDto = new()
        {
            Id = traineeModel.Id,
            FirstName = traineeModel.FirstName,
            LastName = traineeModel.LastName,
            Email = traineeModel.Email,
            Status = traineeModel.Status,
            TechStack = traineeModel.TechStack,
            CreatedAt = traineeModel.CreatedAt,
            UpdatedAt = traineeModel.UpdatedAt
        };

        return traineeResponseDto;
    }
}