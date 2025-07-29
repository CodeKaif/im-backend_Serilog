using IM.Service.V1.Guest.RequestCarService.Impl;
using IM.Service.V1.Guest.RequestCarService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.RequestCarService.Extension
{
    public class RequestCarGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRequestCarGuestSrvc, RequestCarGuestSrvc>();
        }
    }
}
