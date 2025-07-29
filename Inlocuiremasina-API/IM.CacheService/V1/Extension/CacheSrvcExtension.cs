using IM.CacheService.V1.Impl;
using IM.CacheService.V1.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.CacheService.V1.Extension
{
    public class CacheSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICacheSrvc, CacheSrvc>();
        }
    }
}
