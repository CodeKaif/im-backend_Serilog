using IM.Repository.V1.TermConditionsRepository.Impl;
using IM.Repository.V1.TermConditionsRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.TermConditionsRepository.Extension
{
    public class TermConditionsExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITermConditionsRepo, TermConditionsRepo>();
        }
    }
}
