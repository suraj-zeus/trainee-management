
using Trainee.api.Constants;
using Trainee.api.Dto;
using Trainee.api.Models;
using Trainee.api.Repositories;


namespace Trainee.api.Services;

public class TraineeService : ITraineeService
{

    private ITraineeRepository _traineeRepository;
    private IRedisService _redisService;

    public TraineeService(ITraineeRepository traineeRepository, IRedisService redisService)
    {
        _traineeRepository = traineeRepository;
        _redisService = redisService;
    }


    // currently not using this method
    public async Task<List<TraineeResponseDto>> GetAllTrainees()
    {
        List<TraineeModel> trainees = await _traineeRepository.GetTrainees();
        List<TraineeResponseDto> traineesResponse = new List<TraineeResponseDto>();

        foreach (TraineeModel trainee in trainees)
        {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return traineesResponse;
    }


    // currently not using this method
    public async Task<List<TraineeResponseDto>> GetAllTraineesWithSeachParam(string searchParam)
    {
        List<TraineeModel> trainees = await _traineeRepository.GetTraineesWithSearchParam(searchParam);
        List<TraineeResponseDto> traineesResponse = new List<TraineeResponseDto>();

        foreach (TraineeModel trainee in trainees)
        {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return traineesResponse;
    }



    public async Task<PaginationResponseDto<TraineeResponseDto>> GetPaginatedTrainees(PaginationQueryDto paginationQueryDto)
    {
        var (totalRecords, trainees) = await _traineeRepository.GetPaginatedTrainees(paginationQueryDto);
        List<TraineeResponseDto> traineesResponse = new List<TraineeResponseDto>();

        foreach (TraineeModel trainee in trainees)
        {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return MapToPaginatedTraineeResponse(traineesResponse, paginationQueryDto, totalRecords);
    }



    public async Task<TraineeResponseDto> GetTraineeById(int id)
    {

        // first search in cache 
        string redisKey = RedisCacheKeys.Trainee(id);
        TraineeResponseDto traineeResp = await _redisService.GetAsync<TraineeResponseDto>(redisKey);

        if(traineeResp != null)
            return traineeResp;

        // if not found in cache, get it from db
        TraineeModel trainee = await _traineeRepository.GetById(id);

        if (trainee == null)
            return null;

        traineeResp = MapTraineeModelToTraineeResponseDto(trainee);

        // add in cache
        await _redisService.SetAsync<TraineeResponseDto>(redisKey, traineeResp);
        return traineeResp;
    }

    public async Task<TraineeResponseDto> AddTrainee(CreateTraineeDto createTraineeDto)
    {
        TraineeModel trainee = new()
        {
            FirstName = createTraineeDto.FirstName,
            LastName = createTraineeDto.LastName,
            Email = createTraineeDto.Email,
            TechStack = createTraineeDto.TechStack,
            Status = createTraineeDto.Status
        };

        // set timestamps
        trainee.CreatedDate = DateTime.UtcNow;
        trainee.UpdatedDate = DateTime.UtcNow;

        await _traineeRepository.Add(trainee);
        return MapTraineeModelToTraineeResponseDto(trainee);
    }


    public async Task<bool> DeleteTraineeById(int id)
    {
        // delete the trainee record from cache
        string redisKey = RedisCacheKeys.Trainee(id);
        await _redisService.RemoveAsync(redisKey);

        TraineeModel trainee = await _traineeRepository.GetById(id);

        if (trainee == null)
            return false;

        await _traineeRepository.DeleteById(trainee);
        return true;
    }

    public async Task<TraineeResponseDto> UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id)
    {
        // delete the trainee record from cache
        string redisKey = RedisCacheKeys.Trainee(id);
        await _redisService.RemoveAsync(redisKey);

        TraineeModel trainee = await _traineeRepository.UpdateTraineeById(updateTraineeDto, id);

        if (trainee == null)
            return null;

        return MapTraineeModelToTraineeResponseDto(trainee);
    }


    private PaginationResponseDto<TraineeResponseDto> MapToPaginatedTraineeResponse(List<TraineeResponseDto> traineesResponse, PaginationQueryDto paginationQueryDto, int totalRecords)
    {
        PaginationResponseDto<TraineeResponseDto> paginatedResponse = new()
        {
            PageNumber = paginationQueryDto.PageNumber,
            PageSize = paginationQueryDto.PageSize,
            TotalRecords = totalRecords,
            Data = traineesResponse
        };

        return paginatedResponse;
    }


    private TraineeResponseDto MapTraineeModelToTraineeResponseDto(TraineeModel traineeModel)
    {

        TraineeResponseDto traineeResponseDto = new()
        {
            Id = traineeModel.Id,
            FirstName = traineeModel.FirstName,
            LastName = traineeModel.LastName,
            Email = traineeModel.Email,
            Status = traineeModel.Status,
            TechStack = traineeModel.TechStack,
            CreatedDate = traineeModel.CreatedDate,
            UpdatedDate = traineeModel.UpdatedDate
        };

        return traineeResponseDto;
    }
}