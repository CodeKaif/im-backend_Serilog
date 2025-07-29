using IM.Repository.V1.RequestedReplacementCarRepository.Impl;
using IM.Repository.V1.RequestedReplacementCarRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.RequestedReplacementCarRepository.Extension
{
    public class RequestedReplacementCarExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestedReplacementCarRepo, RequestedReplacementCarRepo>();
        }
    }
}
