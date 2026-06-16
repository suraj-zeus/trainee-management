
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
        private readonly ILogger<TraineesController> _logger;

        // The DI container automatically resolves and provides IUserService here
        public TraineesController(ITraineeService traineeService, ILogger<TraineesController> logger)
        {
            _traineeService = traineeService;
            _logger = logger;
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
            string requestId = HttpContext.TraceIdentifier;
            TraineeResponseDto trainee = await _traineeService.GetTraineeById(id);

            if (trainee == null) {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested trainee record with ID : {id} was not found");
                return NotFound(new { Message = $"Trainee with ID : {id} was not found." });
            }

            return Ok(trainee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            string requestId = HttpContext.TraceIdentifier;
            bool deleted = await _traineeService.DeleteTraineeById(id);

            if (!deleted) {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested trainee record with ID : {id} was not found");
                return NotFound(new { Message = $"Trainee with ID : {id} was not found." });
            }

            _logger.LogInformation($"RequestId : [{requestId}]. The trainee record with ID : {id} deleted successfully");
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TraineeResponseDto>> Add(CreateTraineeDto createTraineeDto)
        {
            string requestId = HttpContext.TraceIdentifier;
            TraineeResponseDto trainee = await _traineeService.AddTrainee(createTraineeDto);

            _logger.LogInformation($"RequestId : [{requestId}]. The trainee record with ID : {trainee.Id} created successfully");
            return CreatedAtAction(
                nameof(GetById),
                new { id = trainee.Id },
                trainee
            );

        }



        [HttpPut("{id}")]
        public async Task<ActionResult<TraineeResponseDto>> Update(int id, UpdateTraineeDto updateTraineeDto)
        {
            string requestId = HttpContext.TraceIdentifier;
            TraineeResponseDto trainee = await _traineeService.UpdateTraineeById(updateTraineeDto, id);

            if (trainee == null)
            {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested trainee record with ID : {id} was not found");
                return NotFound(new { Message = $"Trainee with ID : {id} was not found." });
            }

            _logger.LogInformation($"RequestId : [{requestId}]. The trainee record with ID : {id} updated successfully");
            return Ok(trainee);
        }

    }
}