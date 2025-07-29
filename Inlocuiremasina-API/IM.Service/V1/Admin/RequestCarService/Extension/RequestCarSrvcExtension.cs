using IM.Service.V1.Admin.RequestCarService.Impl;
using IM.Service.V1.Admin.RequestCarService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.RequestCarService.Extension
{
    public class RequestCarSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRequestCarSrvc, RequestCarSrvc>();
        }
    }
}
