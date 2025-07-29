using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Middleware.V1.Request.Model;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace Middleware.V1.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly HashSet<string> _ignoredRoutes;

        public LoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _ignoredRoutes = new HashSet<string> { "/healthCheck", "/FileUpload" };
        }

        public async Task Invoke(HttpContext context, RequestMetadata requestMetadata)
        {
            var stopwatch = Stopwatch.StartNew();
            var originalBodyStream = context.Response.Body;

            try
            {

                await _next(context);
                stopwatch.Stop();

                if (ShouldLog(context.Response.StatusCode, context.Request.Path))
                {
                    LogRequest(context, requestMetadata, stopwatch.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                context.Response.StatusCode = 500; 
                LogError(context, requestMetadata, ex, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        private bool ShouldLog(int statusCode, PathString requestPath)
        {
            var allowedStatusCodes = _configuration.GetSection("Logging:AllowedStatusCodes").Get<int[]>() ?? new int[] { };
            return allowedStatusCodes.Contains(statusCode) && !_ignoredRoutes.Contains(requestPath);
        }

        private void LogRequest(HttpContext context, RequestMetadata requestMetadata, long elapsedMs)
        {
            var logData = new
            {
                ServiceName = _configuration["Logging:ServiceName"] ?? "UnknownService",
                UserId = requestMetadata.userId,
                Language = requestMetadata.lang,
                Method = context.Request.Method,
                Route = context.Request.Path,
                StatusCode = context.Response.StatusCode,
                ResponseTimeMs = elapsedMs,
                Token = context.Request.Headers.TryGetValue("Authorization", out var authHeader) ? authHeader.ToString() : "NoToken"
            };

            using (LogContext.PushProperty("ServiceName", logData.ServiceName))
            using (LogContext.PushProperty("UserId", logData.UserId))
            using (LogContext.PushProperty("AuthorizationToken", logData.Token))
            using (LogContext.PushProperty("Language", logData.Language))
            using (LogContext.PushProperty("Method", logData.Method))
            using (LogContext.PushProperty("Route", logData.Route))
            using (LogContext.PushProperty("StatusCode", logData.StatusCode))
            using (LogContext.PushProperty("ResponseTimeMs", logData.ResponseTimeMs))
            using (LogContext.PushProperty("CorrelationId", requestMetadata.correlationId))
            {
                Log.Information($"Success: {logData.Method} {logData.Route} responded with {logData.StatusCode} in {logData.ResponseTimeMs}ms");
            }
        }

        private void LogError(HttpContext context, RequestMetadata requestMetadata, Exception ex, long elapsedMs)
        {
            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrames()?.FirstOrDefault();
            var fileName = frame?.GetFileName() ?? "UnknownFile";
            var lineNumber = frame?.GetFileLineNumber() ?? 0;
            var statusCode = context.Response.StatusCode != 200 ? context.Response.StatusCode : 500;
            var authorizationToken = context.Request.Headers.TryGetValue("Authorization", out var authHeader) ? authHeader.ToString() : "NoToken";

            using (LogContext.PushProperty("ServiceName", _configuration["Logging:ServiceName"] ?? "UnknownService"))
            using (LogContext.PushProperty("UserId", requestMetadata.userId))
            using (LogContext.PushProperty("Language", requestMetadata.lang))
            using (LogContext.PushProperty("Method", context.Request.Method))
            using (LogContext.PushProperty("Route", context.Request.Path))
            using (LogContext.PushProperty("AuthorizationToken", authorizationToken))
            using (LogContext.PushProperty("StatusCode", statusCode))
            using (LogContext.PushProperty("ErrorMessage", ex.Message))
            using (LogContext.PushProperty("StackTrace", ex.ToString()))
            using (LogContext.PushProperty("FileName", fileName))
            using (LogContext.PushProperty("LineNumber", lineNumber))
            using (LogContext.PushProperty("ResponseTimeMs", elapsedMs))
            using (LogContext.PushProperty("CorrelationId", requestMetadata.correlationId))
            {
                Log.Error($"Error: {context.Request.Method} {context.Request.Path} responded with {statusCode} in {elapsedMs}ms");
            }
        }
    }
}
