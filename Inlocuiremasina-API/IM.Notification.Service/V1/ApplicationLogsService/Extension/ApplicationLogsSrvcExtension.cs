using IM.Notification.Service.V1.ApplicationLogsService.Impl;
using IM.Notification.Service.V1.ApplicationLogsService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Service.V1.ApplicationLogsService.Extension
{
    public class ApplicationLogsSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IApplicationLogsSrvc, ApplicationLogsSrvc>();
        }
    }
}
