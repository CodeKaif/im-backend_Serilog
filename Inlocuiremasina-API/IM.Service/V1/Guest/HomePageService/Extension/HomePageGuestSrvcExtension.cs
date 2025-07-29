using IM.Service.V1.Guest.HomePageService.Impl;
using IM.Service.V1.Guest.HomePageService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.HomePageService.Extension
{
    public class HomePageGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IHomePageGuestSrvc, HomePageGuestSrvc>();
        }
    }
}
