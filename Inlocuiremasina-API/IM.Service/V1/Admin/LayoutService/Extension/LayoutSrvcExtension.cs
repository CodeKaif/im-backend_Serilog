using IM.Service.V1.Admin.LayoutService.Impl;
using IM.Service.V1.Admin.LayoutService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.LayoutService.Extension
{
    public class LayoutSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ILayoutSrvc, LayoutSrvc>();
        }
    }
}
