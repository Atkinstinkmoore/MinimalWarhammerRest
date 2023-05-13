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
                _logger.LogInformation("path: {@Path}, method: {@Method}, traceId: {@TraceId}, durationMs: {@Duration:sfff}",
                    context.Request.Path, context.Request.Method, Activity.Current!.TraceId, DateTimeOffset.UtcNow - Activity.Current.StartTimeUtc );
        }
    }
}