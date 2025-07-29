using Auth.Service.V1.AdminService.Impl;
using Auth.Service.V1.AdminService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Service.V1.AdminService.Extension
{
    public class AdminSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAdminSrvc, AdminSrvc>();
        }
    }
}
