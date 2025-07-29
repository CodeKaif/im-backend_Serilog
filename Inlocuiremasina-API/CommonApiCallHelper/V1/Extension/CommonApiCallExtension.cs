using CommonApiCallHelper.V1.Impl;
using CommonApiCallHelper.V1.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace CommonApiCallHelper.V1.Extension
{
    public class CommonApiCallExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICommonApiCall, CommonApiCall>();
        }
    }
}
