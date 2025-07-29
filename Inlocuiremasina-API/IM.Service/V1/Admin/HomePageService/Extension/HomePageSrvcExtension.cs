using IM.Service.V1.Admin.HomePageService.Impl;
using IM.Service.V1.Admin.HomePageService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.HomePageService.Extension
{
    public class HomePageSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IHomePageSrvc, HomePageSrvc>();
        }
    }
}
