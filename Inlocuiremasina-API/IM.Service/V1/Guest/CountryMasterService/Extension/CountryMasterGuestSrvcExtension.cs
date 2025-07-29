using IM.Service.V1.Guest.CountryMasterService.Impl;
using IM.Service.V1.Guest.CountryMasterService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.CountryMasterService.Extension
{
    public class CountryMasterGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICountryMasterGuestSrvc, CountryMasterGuestSrvc>();
        }
    }
}
