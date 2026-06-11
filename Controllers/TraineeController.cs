
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Trainee.api.dto;
using Trainee.api.Services;
using Trainee.api.Dto;


namespace Trainee.api.Controllers
{


    [Authorize]
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
        public async Task<ActionResult<PaginationResponseDto<TraineeResponseDto>>> GetAll([FromQuery] PaginationQueryDto paginationQueryDto)
        {
            PaginationResponseDto<TraineeResponseDto> paginatedResult = await _traineeService.GetPaginatedTrainees(paginationQueryDto);

            return Ok(paginatedResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TraineeResponseDto>> GetById(int id)
        {
            TraineeResponseDto trainee = await _traineeService.GetTraineeById(id);

            if (trainee == null)
                return NotFound(new { Message = $"Trainee with ID {id} was not found." });

            return Ok(trainee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            bool deleted = await _traineeService.DeleteTraineeById(id);

            if (!deleted)
                return NotFound(new { Message = $"Trainee with ID {id} was not found." });

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
                return NotFound(new { Message = $"Trainee with ID {id} was not found." });
            }

            return Ok(trainee);
        }

    }
}