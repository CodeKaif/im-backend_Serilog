using IM.CacheService.V1.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace IM.CacheService
{
    public class CacheServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        { 
            CacheSrvcExtension.RegisterServices(services);
        }
    }
}