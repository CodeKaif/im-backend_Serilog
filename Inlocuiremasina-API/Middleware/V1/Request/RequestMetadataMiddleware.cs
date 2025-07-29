using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;

namespace Middleware.V1.Request
{
    public class RequestMetadataMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMetadataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, RequestMetadata requestMetadata)
        {
            requestMetadata.lang = context.Request.Headers["Accept-Language"].FirstOrDefault() ?? "en";

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
