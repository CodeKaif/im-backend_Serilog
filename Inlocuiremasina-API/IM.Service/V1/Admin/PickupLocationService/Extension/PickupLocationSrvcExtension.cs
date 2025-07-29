using IM.Service.V1.Admin.PickupLocationService.Impl;
using IM.Service.V1.Admin.PickupLocationService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.PickupLocationService.Extension
{
    public class PickupLocationSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPickupLocationSrvc, PickupLocationSrvc>();
        }
    }
}
