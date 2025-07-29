using Microsoft.Extensions.DependencyInjection;

namespace ResponseWrapper.V1.Extension
{
    public class ResponseWrapperExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ResponseHelper>();
        }
    }
}
