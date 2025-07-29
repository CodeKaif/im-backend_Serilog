using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using System.Collections.Generic;

namespace Middleware.V1.Logging.Extension
{
    public static class LoggingExtension
    {
        public static void RegisterLoggingServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LoggingDb");

            var columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"Message", new RenderedMessageColumnWriter() },
                {"MessageTemplate", new MessageTemplateColumnWriter() },
                {"Level", new LevelColumnWriter(true) },
                {"TimeStamp", new TimestampColumnWriter() },
                {"Exception", new ExceptionColumnWriter() },
                {"Properties", new LogEventSerializedColumnWriter() },
                {"ServiceName", new SinglePropertyColumnWriter("ServiceName") },
                {"UserId", new SinglePropertyColumnWriter("UserId") },
                {"Language", new SinglePropertyColumnWriter("Language") },
                {"Method", new SinglePropertyColumnWriter("Method") },
                {"Route", new SinglePropertyColumnWriter("Route") },
                {"StatusCode", new SinglePropertyColumnWriter("StatusCode") },
                {"AuthorizationToken", new SinglePropertyColumnWriter("AuthorizationToken") },
                {"ResponseTimeMs", new SinglePropertyColumnWriter("ResponseTimeMs") },
                {"ErrorMessage", new SinglePropertyColumnWriter("ErrorMessage") },
                {"StackTrace", new SinglePropertyColumnWriter("StackTrace") },
                {"FileName", new SinglePropertyColumnWriter("FileName") },
                {"LineNumber", new SinglePropertyColumnWriter("LineNumber") },
                {"CorrelationId", new SinglePropertyColumnWriter("CorrelationId") }
            };

            Log.Logger = new LoggerConfiguration()
                  .Enrich.FromLogContext()
                  .Enrich.WithThreadId()
                  .MinimumLevel.Verbose() //Ensures ALL logs are captured
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Prevents noise from framework logs
                  .WriteTo.Debug()
                  .WriteTo.Console()
                  .WriteTo.PostgreSQL(
                      connectionString: connectionString,
                      tableName: "ApplicationLogs",
                      needAutoCreateTable: true,
                      columnOptions: columnWriters,
                      restrictedToMinimumLevel: LogEventLevel.Verbose)
                  .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });
        }

        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
