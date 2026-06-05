
using Trainee.api.dto;
using Trainee.api.Interfaces;
using Trainee.api.Models;
using Trainee.api.Repositories;


namespace Trainee.api.Services;

public class TraineeService : ITraineeService
{
    public List<TraineeResponseDto> GetAllTrainees()
    {
        List<TraineeModel> trainees = TraineeRepository.GetTrainees();
        List<TraineeResponseDto> traineesResponse =  new List<TraineeResponseDto>();

        foreach(TraineeModel trainee in trainees) {
            traineesResponse.Add(MapTraineeModelToTraineeResponseDto(trainee));
        }

        return traineesResponse;
    }

    public TraineeResponseDto GetTraineeById(int id)
    {
        TraineeModel trainee = TraineeRepository.GetById(id);

        if(trainee == null)
            return null;
        return MapTraineeModelToTraineeResponseDto(trainee);
    }

    public TraineeResponseDto AddTrainee(CreateTraineeDto createTraineeDto)
    {
        TraineeModel trainee = new()
        {
            FirstName = createTraineeDto.FirstName,
            LastName = createTraineeDto.LastName,
            Email = createTraineeDto.Email,
            TechStack = createTraineeDto.TechStack,
            Status = createTraineeDto.Status
        };

        // set ids and timestamps
        TraineeRepository.IncrementLastId();
        trainee.Id = TraineeRepository.GetLastId();
        trainee.CreatedAt = DateTime.Now;
        trainee.UpdatedAt = DateTime.Now;

        TraineeRepository.Add(trainee);
        return MapTraineeModelToTraineeResponseDto(trainee);
    }


    public bool DeleteTraineeById(int id)
    {
        TraineeModel trainee = TraineeRepository.GetById(id);

        if (trainee == null)
            return false;

        TraineeRepository.DeleteById(trainee);
        return true;
    }

    public TraineeResponseDto UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id)
    {
        TraineeModel trainee = TraineeRepository.GetById(id);
        if (trainee == null)
            return null;

        trainee.FirstName = updateTraineeDto.FirstName;
        trainee.LastName = updateTraineeDto.LastName;
        trainee.Email = updateTraineeDto.Email;
        trainee.TechStack = updateTraineeDto.TechStack;
        trainee.Status = updateTraineeDto.Status;

        // only update updated at timestamp
        trainee.UpdatedAt = DateTime.Now;
        return MapTraineeModelToTraineeResponseDto(trainee);
    }

    public TraineeResponseDto MapTraineeModelToTraineeResponseDto(TraineeModel traineeModel) {

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