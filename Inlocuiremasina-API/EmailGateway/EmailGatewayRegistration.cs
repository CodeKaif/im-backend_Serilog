using EmailGateway.V1.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace EmailGateway
{
    public static class EmailGatewayRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            EmailExtension.RegisterServices(services);
        }
    }
}
