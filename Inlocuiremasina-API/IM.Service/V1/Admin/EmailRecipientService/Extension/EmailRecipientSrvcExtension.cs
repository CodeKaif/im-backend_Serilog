using IM.Service.V1.Admin.EmailRecipientService.Impl;
using IM.Service.V1.Admin.EmailRecipientService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.EmailRecipientService.Extension
{
    public class EmailRecipientSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailRecipientSrvc, EmailRecipientSrvc>();
        }
    }
}
