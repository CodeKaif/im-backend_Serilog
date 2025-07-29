using IM.Service.V1.Admin.CarCompanyService.Impl;
using IM.Service.V1.Admin.CarCompanyService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.CarCompanyService.Extension
{
    public class CarCompanySrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICarCompanySrvc, CarCompanySrvc>();
        }
    }
}
