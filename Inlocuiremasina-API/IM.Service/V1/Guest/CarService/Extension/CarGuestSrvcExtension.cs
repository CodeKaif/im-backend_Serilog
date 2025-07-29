using IM.Service.V1.Guest.CarService.Impl;
using IM.Service.V1.Guest.CarService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.CarService.Extension
{
    public class CarGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICarGuestSrvc, CarGuestSrvc>();
        }
    }
}
