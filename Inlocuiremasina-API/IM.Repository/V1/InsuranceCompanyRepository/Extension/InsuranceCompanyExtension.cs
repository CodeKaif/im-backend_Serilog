using IM.Repository.V1.InsuranceCompanyRepository.Impl;
using IM.Repository.V1.InsuranceCompanyRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.InsuranceCompanyRepository.Extension
{
    public class InsuranceCompanyExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IInsuranceCompanyRepo, InsuranceCompanyRepo>();
        }
    }
}
