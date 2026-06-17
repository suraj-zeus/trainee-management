
using System.ComponentModel.DataAnnotations;

namespace Trainee.api.Dto;


public class PaginationQueryDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be 1 or greater.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
    public int PageSize { get; set; } = 5;

    public string? Search { get; set; }

    [EnumDataType(typeof(TraineeStatus), ErrorMessage = "Invalid Status. Allowed values are: Active, Inactive, and Completed")]
    public string? Status { get; set; }

}