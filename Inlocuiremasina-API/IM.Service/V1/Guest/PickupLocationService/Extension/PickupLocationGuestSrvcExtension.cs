using IM.Service.V1.Guest.PickupLocationService.Impl;
using IM.Service.V1.Guest.PickupLocationService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.PickupLocationService.Extension
{
    public class PickupLocationGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPickupLocationGuestSrvc, PickupLocationGuestSrvc>();
        }
    }
}
