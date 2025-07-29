using Microsoft.Extensions.DependencyInjection;

namespace Localization.V1.Extension
{
    public class LocalizationExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<JsonLocalizationService>();
        }
    }
}
