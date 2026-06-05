
using System;
using Microsoft.AspNetCore.Mvc;

using Trainee.api.Models;
using Trainee.api.dto;
using Trainee.api.Interfaces;

namespace Trainee.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TraineesController : ControllerBase
    {

        private readonly ITraineeService iTraineeService;

        // The DI container automatically resolves and provides IUserService here
        public TraineesController(ITraineeService traineeService)
        {
            iTraineeService = traineeService;
        }

       

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(iTraineeService.GetAllTrainees());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TraineeResponseDto trainee = iTraineeService.GetTraineeById(id);

            if (trainee == null)
            {
                return NotFound();
            }

            return Ok(trainee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            bool deleted = iTraineeService.DeleteTraineeById(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Add(CreateTraineeDto createTraineeDto)
        {

            TraineeResponseDto trainee = iTraineeService.AddTrainee(createTraineeDto);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = trainee.Id }, 
                new { message = "Data Added Successfully", data = trainee }
            );

        }




        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTraineeDto updateTraineeDto)
        {
            TraineeResponseDto trainee = iTraineeService.UpdateTraineeById(updateTraineeDto, id);

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