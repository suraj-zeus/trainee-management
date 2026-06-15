
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


using Trainee.api.dto;
using Trainee.api.Services;

namespace Trainee.api.Controllers;


[Authorize]
[ApiController]
[Route("api/task-assignments")]
public class TaskAssignmentController: ControllerBase 
{
    private ITaskAssignmentService _service;

    public TaskAssignmentController(ITaskAssignmentService service)
    {
        _service = service;
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
        TaskAssignmentResponseDto? taskAssignment = await _service.GetTaskAssignmentById(id);

        if(taskAssignment == null)
        {
            return NotFound(new { message = $"TaskAssignment with id : {id} not found" });
        }

        return Ok(taskAssignment);
    }   

    // POST /api/TaskAssignments
    [HttpPost]
    public async Task<ActionResult<TaskAssignmentResponseDto>> CreateTaskAssignment(CreateTaskAssignmentDto createTaskAssignmentDto)
    {
        TaskAssignmentResponseDto? taskAssignmentResponse = await _service.CreateTaskAssignment(createTaskAssignmentDto);

        if(taskAssignmentResponse == null)
        {
            return BadRequest();
        }

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