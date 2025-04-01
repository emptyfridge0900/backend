

using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _requestDelegate;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _requestDelegate = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate.Invoke(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400;

            _logger.LogError(ex.InnerException ?? ex, ex.InnerException?.Message ?? ex.Message);
            _logger.LogError(ex.InnerException?.StackTrace ?? ex.StackTrace);

            var response = new
            {
                ResponseStatus = new
                {
                    ErrorCode = ex.GetType().Name,
                    Message = ex.Message,
                    Errors = new List<object> { }
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}