


using Trainee.api.ValidationAttributes;
using System.ComponentModel.DataAnnotations;


namespace Trainee.api.Dto;



public class CreateSubmissionFileDto
{
    public IFormFile? formFile {get; set; }
}