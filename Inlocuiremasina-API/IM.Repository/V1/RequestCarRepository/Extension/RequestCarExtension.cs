using IM.Repository.V1.RequestCarRepository.Impl;
using IM.Repository.V1.RequestCarRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.RequestCarRepository.Extension
{
    public class RequestCarExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestCarRepo, RequestCarRepo>();
        }
    }
}
