using EmailGateway;
using IM.Notification.Data;
using IM.Notification.Repository.V1;
using IM.Notification.Service;
using IM.Notification.Service.V1.EmailService.Extension;
using IM.Notification.ServiceExtension.CorsExtension;
using IM.Notification.ServiceExtension.Extension;
using IM.Notification.ServiceExtension.MapperExtension;
using IM.Notification.ServiceExtension.SwaggerExtension;
using Localization.V1.Extension;
using Middleware.V1.Logging.Extension;
using Middleware.V1.Request.Extension;
using ResponseWrapper.V1.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
CorsExtension.RegisterServices(builder.Services);

// swagger register services
SwaggerExtension.RegisterServices(builder.Services);

// Database context registration
ContextRegistration.ContextServices(builder.Services, builder.Configuration);

// Register configuration
ConfigurationExtension.RegisterConfiguration(builder.Services, builder.Configuration);

// Register services from the Repository project
RepositoryRegistration.RegisterServices(builder.Services);

// Register RequestMetadata as a Scoped service
RequestMetadataExtension.RegisterServices(builder.Services);

//EMAIL REGISTRATION
EmailGatewayRegistration.RegisterServices(builder.Services);

// Mapper registration
MapperExtension.RegisterServices(builder.Services);

//Service Registration
EmailSrvcExtension.RegisterServices(builder.Services);
ServiceRegistration.RegisterServices(builder.Services);

// Response Wrapper registration
ResponseWrapperExtension.RegisterServices(builder.Services);

// Logging registration
LoggingExtension.RegisterLoggingServices(builder.Services, builder.Configuration);

// Localization registration
LocalizationExtension.RegisterServices(builder.Services);

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
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseRequestMetadata();
app.UseHttpsRedirection();
app.UseLoggingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
