using IM.Service.V1.Guest.TermConditionsService.Impl;
using IM.Service.V1.Guest.TermConditionsService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.TermConditionsService.Extension
{
    public class TermConditionsGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ITermConditionsGuestSrvc, TermConditionsGuestSrvc>();
        }
    }
}
