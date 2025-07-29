using Auth.Repository.V1.MasterRoleRepositorey.Impl;
using Auth.Repository.V1.MasterRoleRepositorey.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.CarCompanyRepository.Extension
{
    public class MasterRoleExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMasterRoleRepo, MasterRoleRepo>();
        }
    }
}
