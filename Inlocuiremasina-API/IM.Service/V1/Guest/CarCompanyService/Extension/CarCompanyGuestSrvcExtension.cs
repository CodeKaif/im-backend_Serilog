using IM.Service.V1.Guest.CarCompanyService.Impl;
using IM.Service.V1.Guest.CarCompanyService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.CarCompanyService.Extension
{
    public class CarCompanyGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICarCompanyGuestSrvc, CarCompanyGuestSrvc>();
        }
    }
}
