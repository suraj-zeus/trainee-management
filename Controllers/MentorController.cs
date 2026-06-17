using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

using Trainee.api.Dto;
using Trainee.api.Services;

namespace Trainee.api.Controllers
{


    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MentorsController : ControllerBase
    {

        private readonly IMentorService _mentorService;
        private readonly ILogger<MentorsController> _logger;

        public MentorsController(IMentorService mentorService, ILogger<MentorsController> logger)
        {
            _mentorService = mentorService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<List<MentorResponseDto>>> GetAll()
        {
            List<MentorResponseDto> mentors = await _mentorService.GetAllMentors();

            return Ok(mentors);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MentorResponseDto>> GetById(int id)
        {
            string requestId = HttpContext.TraceIdentifier;
            MentorResponseDto mentor = await _mentorService.GetMentorById(id);

            if (mentor == null)
            {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested mentor record with ID : {id} was not found");
                return NotFound(new { Message = $"Mentor with ID : {id} was not found." });
            }

            return Ok(mentor);
        }


        [HttpPost]
        public async Task<ActionResult<MentorResponseDto>> Add(CreateMentorDto createMentorDto)
        {
            string requestId = HttpContext.TraceIdentifier;
            MentorResponseDto mentor = await _mentorService.AddMentor(createMentorDto);

            _logger.LogInformation($"RequestId : [{requestId}]. Mentor record with ID : {mentor.Id} created successfully");

            return CreatedAtAction(
                nameof(GetById),
                new { id = mentor.Id },
                mentor
            );

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            string requestId = HttpContext.TraceIdentifier;
            bool deleted = await _mentorService.DeleteMentorById(id);

            if (!deleted)
            {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested mentor record with ID : {id} was not found");
                return NotFound(new { Message = $"Mentor with ID : {id} was not found." });
            }

            _logger.LogInformation($"RequestId : [{requestId}]. Mentor record with ID : {id} deleted successfully");
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<MentorResponseDto>> Update(int id, UpdateMentorDto updateMentorDto)
        {
            string requestId = HttpContext.TraceIdentifier;
            MentorResponseDto mentor = await _mentorService.UpdateMentorById(updateMentorDto, id);

            if (mentor == null)
            {
                _logger.LogInformation($"RequestId : [{requestId}]. The requested mentor record with ID : {id} was not found");
                return NotFound(new { Message = $"Mentor with ID : {id} was not found." });
            }

            _logger.LogInformation($"RequestId : [{requestId}]. Mentor record with ID : {id} updated successfully");
            return Ok(mentor);
        }
    }
}