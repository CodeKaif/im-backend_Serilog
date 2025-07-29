using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Middleware.V1.Request.Model;

namespace Middleware.V1.Request.Extension
{
    public static class RequestMetadataExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<RequestMetadata>();
        }

        public static IApplicationBuilder UseRequestMetadata(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestMetadataMiddleware>();
        }

        public static IApplicationBuilder UseJwtUserIdExtractor(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExtractUserIdMiddleware>();
        }
    }
}
