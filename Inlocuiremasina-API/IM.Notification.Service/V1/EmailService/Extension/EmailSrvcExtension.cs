using IM.Notification.Service.V1.EmailService.Impl;
using IM.Notification.Service.V1.EmailService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Service.V1.EmailService.Extension
{
    public class EmailSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSrvc, EmailSrvc>();
        }
    }
}
