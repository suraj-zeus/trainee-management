using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Trainee.api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }



    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error occurred: {Message}", exception.Message);


        // Handle multiple exceptions in one place
        var (statusCode, title) = exception switch
        {
            ForbiddenException => (StatusCodes.Status403Forbidden, "Forbidden"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
            BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid Input"),
            InvalidOperationException => (StatusCodes.Status400BadRequest, "Invalid State Operation"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; 
    }
}