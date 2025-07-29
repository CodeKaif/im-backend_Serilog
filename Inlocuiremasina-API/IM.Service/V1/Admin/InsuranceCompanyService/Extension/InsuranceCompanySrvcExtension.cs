using IM.Service.V1.Admin.InsuranceCompanyService.Impl;
using IM.Service.V1.Admin.InsuranceCompanyService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.InsuranceCompanyService.Extension
{
    public class InsuranceCompanySrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IInsuranceCompanySrvc, InsuranceCompanySrvc>();
        }
    }
}
