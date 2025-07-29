using IM.Repository.V1.PrivacyPolicyRepository.Impl;
using IM.Repository.V1.PrivacyPolicyRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.PrivacyPolicyRepository.Extension
{
    public class PrivacyPolicyExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPrivacyPolicyRepo, PrivacyPolicyRepo>();
        }
    }
}
