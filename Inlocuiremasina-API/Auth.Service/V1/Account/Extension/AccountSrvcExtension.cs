using Auth.Service.V1.Account.Impl;
using Auth.Service.V1.Account.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Service.V1.Account.Extension
{
    public class AccountSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAccountSrvc, AccountSrvc>();
        }
    }
}
