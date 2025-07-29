using IM.Repository.V1.HomePageRepository.Impl;
using IM.Repository.V1.HomePageRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.HomePageRepository.Extension
{
    public class HomePageExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IHomePageRepo, HomePageRepo>();
        }
    }
}
