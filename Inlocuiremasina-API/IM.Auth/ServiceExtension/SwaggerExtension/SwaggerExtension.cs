using Microsoft.OpenApi.Models;

namespace IM.Auth.ServiceExtension.SwaggerExtension
{
    public static class SwaggerExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info.Version = "V1";
                    document.Info.Title = "IM.Auth.API";
                    document.Info.Description = "-";

                    if (document.Components == null)
                    {
                        document.Components = new OpenApiComponents();
                    }

                    // AddSecurityDefinition equivalent
                    document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                    });

                    document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                    
                    // Global Accept-Language header for all endpoints
                    foreach (var path in document.Paths.Values)
                    {
                        foreach (var operation in path.Operations.Values)
                        {
                            operation.Parameters ??= new List<OpenApiParameter>();

                            operation.Parameters.Add(new OpenApiParameter
                            {
                                Name = "Accept-Language",
                                In = ParameterLocation.Header,
                                Required = false,
                                Schema = new OpenApiSchema
                                {
                                    Type = "string",
                                    Default = new Microsoft.OpenApi.Any.OpenApiString("en")
                                },
                                Description = "Example: 'en', 'fr', 'es'."
                            });
                        }
                    }

                    return Task.CompletedTask;
                });

            });

        }
    }
}
