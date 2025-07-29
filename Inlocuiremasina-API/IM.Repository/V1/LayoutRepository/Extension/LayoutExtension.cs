using IM.Repository.V1.LayoutRepository.Impl;
using IM.Repository.V1.LayoutRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.LayoutRepository.Extension
{
    public class LayoutExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILayoutRepo, LayoutRepo>();
        }
    }
}
