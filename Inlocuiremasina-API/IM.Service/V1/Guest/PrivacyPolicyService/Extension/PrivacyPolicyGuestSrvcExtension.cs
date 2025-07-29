using IM.Service.V1.Guest.PrivacyPolicyService.Impl;
using IM.Service.V1.Guest.PrivacyPolicyService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Guest.PrivacyPolicyService.Extension
{
    public class PrivacyPolicyGuestSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPrivacyPolicyGuestSrvc, PrivacyPolicyGuestSrvc>();
        }
    }
}
