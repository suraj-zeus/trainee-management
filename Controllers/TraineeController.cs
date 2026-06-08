
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
        public async Task<IActionResult> GetAll(string? search)
        {

            List<TraineeResponseDto> trainees = !string.IsNullOrEmpty(search)
                                        ? await _traineeService.GetAllTraineesWithSeachParam(search)
                                        : await _traineeService.GetAllTrainees();

            return Ok(trainees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TraineeResponseDto trainee = await _traineeService.GetTraineeById(id);

            if (trainee == null)
            {
                return NotFound();
            }

            return Ok(trainee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool deleted = await _traineeService.DeleteTraineeById(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateTraineeDto createTraineeDto)
        {

            TraineeResponseDto trainee = await _traineeService.AddTrainee(createTraineeDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = trainee.Id },
                new { message = "Data Added Successfully", data = trainee }
            );

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTraineeDto updateTraineeDto)
        {
            TraineeResponseDto trainee = await _traineeService.UpdateTraineeById(updateTraineeDto, id);

            if (trainee == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Data updated Successfully",
                data = trainee,
            });
        }

    }
}