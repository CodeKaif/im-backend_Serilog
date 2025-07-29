using IM.Notification.Repository.V1.CarCompanyRepository.Impl;
using IM.Notification.Repository.V1.CarCompanyRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Repository.V1.CarCompanyRepository.Extension
{
    public class ApplicationLogsExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IApplicationLogsRepo, ApplicationLogsRepo>();
        }
    }
}
