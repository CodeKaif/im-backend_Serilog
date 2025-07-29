using IM.Notification.Repository.Core.Generic.Extension;
using IM.Notification.Repository.V1.CarCompanyRepository.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Repository.V1
{
    public class RepositoryRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Register services here

            // Generic Repository Registration
            GenericExtension.RegisterServices(services);

            // Application Logs Repository Registration
            ApplicationLogsExtension.RegisterServices(services);
        }
    }
}
