
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


using Trainee.api.Dto;
using Trainee.api.Services;

namespace Trainee.api.Controllers;


[Authorize]
[ApiController]
[Route("api/task-assignments")]
public class TaskAssignmentController: ControllerBase 
{
    private ITaskAssignmentService _service;
    private readonly ILogger<TaskAssignmentController> _logger;

    public TaskAssignmentController(ITaskAssignmentService service, ILogger<TaskAssignmentController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET /api/TaskAssignments
    [HttpGet]
    public async Task<ActionResult<List<TaskAssignmentResponseDto>>> GetAllTaskAssignments()
    {
        List<TaskAssignmentResponseDto> taskAssignments  = await _service.GetAllTaskAssignments();
        return Ok(taskAssignments);
    }

    // GET /api/TaskAssignments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskAssignmentResponseDto>> GetTaskAssignmentById(int id)
    {
        string requestId = HttpContext.TraceIdentifier;
        TaskAssignmentResponseDto? taskAssignment = await _service.GetTaskAssignmentById(id);

        if(taskAssignment == null)
        {
             _logger.LogInformation($"RequestId : [{requestId}]. The requested task assignment record with ID : {id} was not found");
            return NotFound(new { message = $"TaskAssignment with id : {id} not found" });
        }

        return Ok(taskAssignment);
    }   

    // POST /api/TaskAssignments
    [HttpPost]
    public async Task<ActionResult<TaskAssignmentResponseDto>> CreateTaskAssignment(CreateTaskAssignmentDto createTaskAssignmentDto)
    {
        string requestId = HttpContext.TraceIdentifier;
        TaskAssignmentResponseDto taskAssignmentResponse = await _service.CreateTaskAssignment(createTaskAssignmentDto);

         _logger.LogInformation($"RequestId : [{requestId}]. The task assignment record with ID : {taskAssignmentResponse.Id} was created successfully..");
        return Ok(taskAssignmentResponse);
    }

    // PUT /api/TaskAssignments/{id}
    [HttpPut("{id}/status")]
    public async Task<ActionResult<TaskAssignmentResponseDto>> UpdateTaskAssignmentDetails(int id, UpdateTaskAssignmentDto updateTaskAssignmentDto)
    {
        TaskAssignmentResponseDto updatedTaskAssignment = await _service.UpdateTaskAssignmentDetails(id, updateTaskAssignmentDto);

        return Ok(updatedTaskAssignment);
    }
}