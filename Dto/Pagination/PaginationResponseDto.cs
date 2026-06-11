

namespace Trainee.api.Dto;


public class PaginationResponseDto<T>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public int TotalRecords {get; set;} = 1;

    public List<T> Data {get; set;}
}