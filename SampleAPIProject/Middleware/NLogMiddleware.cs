using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class NLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<NLogMiddleware> _logger;

    public NLogMiddleware(RequestDelegate next, ILogger<NLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the request
        _logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");

        // Call the next middleware in the pipeline
        await _next(context);

        // Log the response
        _logger.LogInformation($"Finished handling request: {context.Response.StatusCode}");
    }
}
