using IM.Service.V1.Guest.RequestedReplacementCarService.Impl;
using IM.Service.V1.Guest.RequestedReplacementCarService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.RequestedReplacementCarService.Extension
{
    public class RequestedReplacementCarGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRequestedReplacementCarGuestSrvc, RequestedReplacementCarGuestSrvc>();
        }
    }
}
