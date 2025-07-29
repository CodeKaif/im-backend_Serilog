using EmailGateway.V1.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EmailGateway.V1.Extension
{
    public static class EmailExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEmailGateway, EmailGateway.V1.Impl.EmailGateway>();
        }
    }
}
