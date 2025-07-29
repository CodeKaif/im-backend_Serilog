using IM.Service.V1.Admin.PrivacyPolicyService.Impl;
using IM.Service.V1.Admin.PrivacyPolicyService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.PrivacyPolicyService.Extension
{
    public class PrivacyPolicySrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPrivacyPolicySrvc, PrivacyPolicySrvc>();
        }
    }
}
