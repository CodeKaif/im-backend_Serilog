using EmailGateway.V1.Extension;
using IM.Notification.Service.V1.ApplicationLogsService.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Service
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            EmailExtension.RegisterServices(services);

            ApplicationLogsSrvcExtension.RegisterServices(services);
        }
    }
}
