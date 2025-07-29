using IM.Repository.V1.CountryMasterRepository.Impl;
using IM.Repository.V1.CountryMasterRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.CountryMasterRepository.Extension
{
    public class CountryMasterExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICountryMasterRepo, CountryMasterRepo>();
        }
    }
}
