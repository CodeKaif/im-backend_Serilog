using CommonApiCallHelper;
using FileUploader;
using IM.CacheService;
using IM.Content.Extensions;
using IM.Content.ServiceExtension.AuthExtension;
using IM.Content.ServiceExtension.CorsExtension;
using IM.Content.ServiceExtension.MapperExtension;
using IM.Content.ServiceExtension.SwaggerExtension;
using IM.Data;
using IM.Repository.V1;
using IM.Service;
using Localization.V1.Extension;
using Middleware.V1.Logging.Extension;
using Middleware.V1.Request.Extension;
using ResponseWrapper.V1.Extension;

var builder = WebApplication.CreateBuilder(args);

CorsExtension.RegisterServices(builder.Services);

// Register configuration
ConfigurationExtension.RegisterConfiguration(builder.Services, builder.Configuration);

// swagger register services
SwaggerExtension.RegisterServices(builder.Services);

// Database context registration
ContextRegistration.ContextServices(builder.Services, builder.Configuration);

// Register RequestMetadata as a Scoped service
RequestMetadataExtension.RegisterServices(builder.Services);

// Auth service registration
AuthExtension.RegisterServices(builder.Services, builder.Configuration);

// Register services from the Service project
ServiceRegistration.RegisterServices(builder.Services);

// Cache service registration
CacheServiceRegistration.RegisterServices(builder.Services);

// Register services from the Repository project
RepositoryRegistration.RegisterServices(builder.Services);

// Response Wrapper registration
ResponseWrapperExtension.RegisterServices(builder.Services);

// Localization registration
LocalizationExtension.RegisterServices(builder.Services);

// Mapper registration
MapperExtension.RegisterServices(builder.Services);

// File Uploader registration
FileUploaderRegistration.RegisterServices(builder.Services);

// Internal Client Request Configuration
CommonApiCallRegistration.RegisterServices(builder.Services);

// Logging registration
LoggingExtension.RegisterLoggingServices(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));
//}

// Use the middleware via the extension method
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseRequestMetadata(); // Language Middleware
app.UseJwtUserIdExtractor(); // User ID Middleware
app.UseLoggingMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
