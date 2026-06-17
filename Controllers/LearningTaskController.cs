

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

using Trainee.api.Dto;
using Trainee.api.Services;

namespace Trainee.api.Controllers
{

    
    [Authorize]
    [ApiController]
    [Route("api/learning-tasks")]
    public class LearningTasksController : ControllerBase
    {

        private readonly ILearningTaskService _learningTaskService;
        private readonly ILogger<LearningTasksController> _logger;

        public LearningTasksController(ILearningTaskService learningTaskService, ILogger<LearningTasksController> logger)
        {
            _logger = logger;
            _learningTaskService = learningTaskService;
        }


        [HttpGet]
        public async Task<ActionResult<List<LearningTaskResponseDto>>> GetAll()
        {
            List<LearningTaskResponseDto> learningTasks = await _learningTaskService.GetAllLearningTasks();

            return Ok(learningTasks);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LearningTaskResponseDto>> GetById(int id)
        {
             string requestId = HttpContext.TraceIdentifier;
            LearningTaskResponseDto learningTask = await _learningTaskService.GetLearningTaskById(id);

            if (learningTask == null) {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested learning task record with ID : {id} was not found");
                return NotFound(new { Message = $"Learning Task with ID : {id} was not found." });
            }

            return Ok(learningTask);
        }


        [HttpPost]
        public async Task<ActionResult<LearningTaskResponseDto>> Add(CreateLearningTaskDto createLearningTaskDto)
        {
            string requestId = HttpContext.TraceIdentifier;
            LearningTaskResponseDto learningTaskResponse = await _learningTaskService.AddNewLearningTask(createLearningTaskDto);

            _logger.LogInformation($"RequestId : [{requestId}].  The learning task record with ID : {learningTaskResponse.Id} created successfully");

            return CreatedAtAction(
                nameof(GetById),
                new { id = learningTaskResponse.Id },
                learningTaskResponse
            );

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            string requestId = HttpContext.TraceIdentifier;
            bool deleted = await _learningTaskService.DeleteLearningTaskById(id);

            if (!deleted) {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested learning task record with ID : {id} was not found");
                return NotFound(new { Message = $"Learning task with ID : {id} was not found." });
            }

            _logger.LogInformation($"RequestId : [{requestId}]. Learing task record with ID : {id} deleted successfully");
            return NoContent();
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<LearningTaskResponseDto>> Update(int id, UpdateLearningTaskDto updateLearningTaskDto)
        {
            string requestId = HttpContext.TraceIdentifier;
            LearningTaskResponseDto learningTaskResponse = await _learningTaskService.UpdateLearningTaskById(updateLearningTaskDto, id);

            if (learningTaskResponse == null)
            {
                _logger.LogInformation($"RequestId : [{requestId}].  The requested learning task record with ID : {id} was not found");
                return NotFound(new { Message = $"Learning task record with ID : {id} was not found." });
            }

            _logger.LogInformation($"RequestId : [{requestId}].  Learning task record with ID : {id} updated successfully");
            return Ok(learningTaskResponse);
        }
    }
}