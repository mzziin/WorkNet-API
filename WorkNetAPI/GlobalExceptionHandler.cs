using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        _logger.LogError("Something went wrong {exception}", exception);

        var response = new ErrorResponse
        {
            ExceptionMessage = exception.Message,
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Something went wrong"
        };

        await httpContext.Response.WriteAsJsonAsync(response);

        return true;
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string ExceptionMessage { get; set; }
    public string Message { get; set; }
}
