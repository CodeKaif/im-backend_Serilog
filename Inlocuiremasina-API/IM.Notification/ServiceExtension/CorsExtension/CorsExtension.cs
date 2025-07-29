namespace IM.Notification.ServiceExtension.CorsExtension
{
    public static class CorsExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                                .SetIsOriginAllowed((host) => true)
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials());
            });
        }
    }
}
