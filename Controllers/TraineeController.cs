
using System;
using Microsoft.AspNetCore.Mvc;

using Trainee.api.dto;
using Trainee.api.Services;

namespace Trainee.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TraineesController : ControllerBase
    {

        private readonly ITraineeService _traineeService;

        // The DI container automatically resolves and provides IUserService here
        public TraineesController(ITraineeService traineeService)
        {
            _traineeService = traineeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TraineeResponseDto>>> GetAll(string? search)
        {
            List<TraineeResponseDto> trainees = !string.IsNullOrEmpty(search)
                                        ? await _traineeService.GetAllTraineesWithSeachParam(search)
                                        : await _traineeService.GetAllTrainees();

            return Ok(trainees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TraineeResponseDto>> GetById(int id)
        {
            TraineeResponseDto trainee = await _traineeService.GetTraineeById(id);

            if (trainee == null)
                return NotFound(new {Message = $"Trainee with ID {id} was not found."});

            return Ok(trainee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            bool deleted = await _traineeService.DeleteTraineeById(id);

            if (!deleted)
                return NotFound(new {Message = $"Trainee with ID {id} was not found."});

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TraineeResponseDto>> Add(CreateTraineeDto createTraineeDto)
        {

            TraineeResponseDto trainee = await _traineeService.AddTrainee(createTraineeDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = trainee.Id },
                trainee
            );

        }



        [HttpPut("{id}")]
        public async Task<ActionResult<TraineeResponseDto>> Update(int id, UpdateTraineeDto updateTraineeDto)
        {
            TraineeResponseDto trainee = await _traineeService.UpdateTraineeById(updateTraineeDto, id);

            if (trainee == null)
            {
                return NotFound(new {Message = $"Trainee with ID {id} was not found."});
            }

            return Ok(trainee);
        }

    }
}