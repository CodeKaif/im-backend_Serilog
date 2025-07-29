using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

namespace Middleware.V1.Logging.Extension
{
    public static class LoggingExtension
    {
        public static void RegisterLoggingServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LoggingDb");

            var sinkOptions = new MSSqlServerSinkOptions
            {
                TableName = "ApplicationLogs",
                AutoCreateSqlTable = true,
                BatchPostingLimit = 1,
                BatchPeriod = TimeSpan.FromMilliseconds(1),
                UseSqlBulkCopy = false,
                EagerlyEmitFirstEvent = true
            };

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn { ColumnName = "ServiceName", DataType = System.Data.SqlDbType.NVarChar, AllowNull = false },
                    new SqlColumn { ColumnName = "UserId", DataType = System.Data.SqlDbType.Int, AllowNull = true },
                    new SqlColumn { ColumnName = "Language", DataType = System.Data.SqlDbType.NVarChar, AllowNull = true },
                    new SqlColumn { ColumnName = "Method", DataType = System.Data.SqlDbType.NVarChar, AllowNull = false },
                    new SqlColumn { ColumnName = "Route", DataType = System.Data.SqlDbType.NVarChar, AllowNull = false },
                    new SqlColumn { ColumnName = "StatusCode", DataType = System.Data.SqlDbType.Int, AllowNull = false },
                    new SqlColumn { ColumnName = "AuthorizationToken", DataType = System.Data.SqlDbType.NVarChar, AllowNull = false },
                    new SqlColumn { ColumnName = "ResponseTimeMs", DataType = System.Data.SqlDbType.Int, AllowNull = true },
                    new SqlColumn { ColumnName = "ErrorMessage", DataType = System.Data.SqlDbType.NVarChar, AllowNull = true },
                    new SqlColumn { ColumnName = "StackTrace", DataType = System.Data.SqlDbType.NVarChar, AllowNull = true },
                    new SqlColumn { ColumnName = "FileName", DataType = System.Data.SqlDbType.NVarChar, AllowNull = true },
                    new SqlColumn { ColumnName = "LineNumber", DataType = System.Data.SqlDbType.Int, AllowNull = true }
                }
            };

            Log.Logger = new LoggerConfiguration()
                  .Enrich.FromLogContext()
                  .Enrich.WithThreadId()
                  .MinimumLevel.Verbose() //Ensures ALL logs are captured
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Prevents noise from framework logs
                  .WriteTo.Debug()
                  .WriteTo.Console()
                  .WriteTo.MSSqlServer(
                      connectionString: connectionString,
                      sinkOptions: sinkOptions,
                      columnOptions: columnOptions,
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
