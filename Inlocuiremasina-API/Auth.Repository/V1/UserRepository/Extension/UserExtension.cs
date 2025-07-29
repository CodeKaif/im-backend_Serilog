using Auth.Repository.V1.UserRepositorey.Impl;
using Auth.Repository.V1.UserRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.UserRepository.Extension
{
    public class UserExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
        }
    }
}
