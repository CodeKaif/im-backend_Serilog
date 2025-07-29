namespace IM.Notification.ServiceExtension.SwaggerExtension
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
                    document.Info.Title = "IM.Notification.API";
                    document.Info.Description = "-";

                    return Task.CompletedTask;
                });

            });
        }
    }
}
