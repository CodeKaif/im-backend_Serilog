using IM.Service.V1.Admin.RequestedReplacementCarService.Impl;
using IM.Service.V1.Admin.RequestedReplacementCarService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.RequestedReplacementCarService.Extension
{
    public class RequestedReplacementCarSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRequestedReplacementCarSrvc, RequestedReplacementCarSrvc>();
        }
    }
}
