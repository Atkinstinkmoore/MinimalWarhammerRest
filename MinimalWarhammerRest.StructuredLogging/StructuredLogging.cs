using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MinimalWarhammerRest.StructuredLogging
{
    public sealed class StructuredLogging
    {
        private readonly ILogger<StructuredLogging> _logger;
        private readonly RequestDelegate _next;

        public StructuredLogging(RequestDelegate next, ILogger<StructuredLogging> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            
            await _next(context);

            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("{@Path}, {@Method}, {@TraceId}, {@DurationMs:sfff}, {@StatusCode}",
                    context.Request.Path, context.Request.Method, context.TraceIdentifier, DateTimeOffset.UtcNow - Activity.Current.StartTimeUtc, context.Response.StatusCode );
        }
    }
}