using IM.Service.V1.Guest.InsuranceCompanyService.Impl;
using IM.Service.V1.Guest.InsuranceCompanyService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.InsuranceCompanyService.Extension
{
    public class InsuranceCompanyGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IInsuranceCompanyGuestSrvc, InsuranceCompanyGuestSrvc>();
        }
    }
}
