using IM.Notification.Repository.Core.Generic.Impl;
using IM.Notification.Repository.Core.Generic.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Repository.Core.Generic.Extension
{
    public class GenericExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        }
    }
}
