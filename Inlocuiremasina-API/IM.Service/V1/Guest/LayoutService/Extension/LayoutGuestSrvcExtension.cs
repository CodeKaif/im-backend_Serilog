using IM.Service.V1.Guest.LayoutService.Impl;
using IM.Service.V1.Guest.LayoutService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.LayoutService.Extension
{
    public class LayoutGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ILayoutGuestSrvc, LayoutGuestSrvc>();
        }
    }
}
