using Auth.Repository.Core.Generic.Impl;
using Auth.Repository.Core.Generic.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Repository.Core.Generic.Extension
{
    public class GenericExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        }
    }
}
