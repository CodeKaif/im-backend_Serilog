using IM.Repository.Core.Generic.Impl;
using IM.Repository.Core.Generic.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.Core.Generic.Extension
{
    public class GenericExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        }
    }
}
