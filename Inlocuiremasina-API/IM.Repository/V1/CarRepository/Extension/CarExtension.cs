using IM.Repository.V1.CarRepository.Impl;
using IM.Repository.V1.CarRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.CarRepository.Extension
{
    public class CarExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICarRepo, CarRepo>();
        }
    }
}
