using IM.Repository.V1.PickupLocationRepository.Impl;
using IM.Repository.V1.PickupLocationRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.PickupLocationRepository.Extension
{
    public class PickupLocationExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPickupLocationRepo, PickupLocationRepo>();
        }
    }
}
