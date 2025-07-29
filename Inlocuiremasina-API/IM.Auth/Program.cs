using Auth.Data;
using Auth.Repository.V1;
using Auth.Service;
using CommonApiCallHelper;
using IM.Auth.Extensions;
using IM.Auth.ServiceExtension.CorsExtension;
using IM.Auth.ServiceExtension.IdentityExtension;
using IM.Auth.ServiceExtension.MapperExtension;
using IM.Auth.ServiceExtension.SwaggerExtension;
using Localization.V1.Extension;
using Middleware.V1.Logging.Extension;
using Middleware.V1.Request.Extension;
using ResponseWrapper.V1.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

CorsExtension.RegisterServices(builder.Services);

// swagger register services
SwaggerExtension.RegisterServices(builder.Services);

// Register Context
ContextRegistration.ContextServices(builder.Services, builder.Configuration);

// Register services from the Repository project
RepositoryRegistration.RegisterServices(builder.Services);

// Register RequestMetadata as a Scoped service
RequestMetadataExtension.RegisterServices(builder.Services);

// Internal Client Request Configuration
CommonApiCallRegistration.RegisterServices(builder.Services);

// Register Identity service
IdentityExtension.RegisterServices(builder.Services, builder.Configuration);

// Register services from the Service project
ServiceRegistration.RegisterServices(builder.Services);

ResponseWrapperExtension.RegisterServices(builder.Services);

// Localization registration
LocalizationExtension.RegisterServices(builder.Services);

ConfigurationExtension.RegisterConfiguration(builder.Services, builder.Configuration);

// Mapper registration
MapperExtension.RegisterServices(builder.Services);

// Logging registration
LoggingExtension.RegisterLoggingServices(builder.Services, builder.Configuration);

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
