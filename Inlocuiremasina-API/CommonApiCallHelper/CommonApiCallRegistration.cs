using CommonApiCallHelper.V1.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace CommonApiCallHelper
{
    public class CommonApiCallRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            CommonApiCallExtension.RegisterServices(services);
        }
    }
}
