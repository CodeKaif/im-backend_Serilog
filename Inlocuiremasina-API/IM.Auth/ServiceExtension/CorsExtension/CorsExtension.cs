namespace IM.Auth.ServiceExtension.CorsExtension
{
    public static class CorsExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(
                    "http://localhost:4200")
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }
    }
}
