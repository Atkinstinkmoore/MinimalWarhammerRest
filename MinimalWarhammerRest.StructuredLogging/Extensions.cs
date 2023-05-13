using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalWarhammerRest.StructuredLogging
{
    public static class Extensions
    {

        public static IServiceCollection AddStructuredLogging(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseStructuredLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<StructuredLogging>();
            return app;
        }
    }
}