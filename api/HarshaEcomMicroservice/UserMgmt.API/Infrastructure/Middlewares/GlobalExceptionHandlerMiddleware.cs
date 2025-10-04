namespace UserMgmt.API.Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, detail) = exception switch
        {
            // HTTP & Network errors
            HttpRequestException => (StatusCodes.Status502BadGateway,
                "Bad Gateway",
                "Failed to communicate with external service"),

            TaskCanceledException => (StatusCodes.Status504GatewayTimeout,
                "Gateway Timeout",
                "The request to external service timed out"),

            // Serialization errors
            JsonException => (StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "Failed to process response data"),

            // Validation & Business Logic errors
            ArgumentException => (StatusCodes.Status400BadRequest,
                "Bad Request",
                exception.Message),

            InvalidOperationException => (StatusCodes.Status400BadRequest,
                "Bad Request",
                exception.Message),

            KeyNotFoundException => (StatusCodes.Status404NotFound,
                "Not Found",
                "The requested resource was not found"),

            UnauthorizedAccessException => (StatusCodes.Status403Forbidden,
                "Forbidden",
                "You don't have permission to access this resource"),

            // General database connection errors
            System.Data.Common.DbException => (StatusCodes.Status503ServiceUnavailable,
                "Service Unavailable",
                "Database is temporarily unavailable"),

            // Timeout errors
            TimeoutException => (StatusCodes.Status408RequestTimeout,
                "Request Timeout",
                "The operation took too long to complete"),

            // Not Supported errors
            NotSupportedException => (StatusCodes.Status400BadRequest,
                "Bad Request",
                "The requested operation is not supported"),

            NotImplementedException => (StatusCodes.Status501NotImplemented,
                "Not Implemented",
                "This feature is not yet implemented"),

            // Default case
            _ => (StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}


public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
