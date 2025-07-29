using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using Serilog.Context;
using System;

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
            requestMetadata.correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            context.Response.Headers["X-Correlation-ID"] = requestMetadata.correlationId;

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", requestMetadata.correlationId))
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
        }
    }
}
