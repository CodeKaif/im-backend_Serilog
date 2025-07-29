using IM.Service.V1.Admin.CarService.Impl;
using IM.Service.V1.Admin.CarService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.CarService.Extension
{
    public class CarSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICarSrvc, CarSrvc>();
        }
    }
}
