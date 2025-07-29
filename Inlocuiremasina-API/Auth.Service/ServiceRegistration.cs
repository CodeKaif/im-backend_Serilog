using Auth.Service.V1.Account.Extension;
using Auth.Service.V1.AdminService.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Service
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Account Service Registration
            AccountSrvcExtension.RegisterServices(services);

            // Admin Service Registration
            AdminSrvcExtension.RegisterServices(services);

        }
    }
}
