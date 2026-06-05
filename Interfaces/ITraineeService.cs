
using Trainee.api.dto;

namespace Trainee.api.Interfaces;


public interface ITraineeService
{
    public List<TraineeResponseDto> GetAllTrainees();

    public TraineeResponseDto GetTraineeById(int id);

    public TraineeResponseDto AddTrainee(CreateTraineeDto createTraineeDto);

    public bool DeleteTraineeById(int id);

    public TraineeResponseDto UpdateTraineeById(UpdateTraineeDto updateTraineeDto, int id);
}