using IM.Repository.V1.CarCompanyRepository.Impl;
using IM.Repository.V1.CarCompanyRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.CarCompanyRepository.Extension
{
    public class CarCompanyExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICarCompanyRepo, CarCompanyRepo>();
        }
    }
}
