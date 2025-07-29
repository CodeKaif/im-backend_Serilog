using Auth.Repository.Core.Generic.Extension;
using IM.Repository.V1.CarCompanyRepository.Extension;
using IM.Repository.V1.UserRepository.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Repository.V1
{
    public class RepositoryRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Generic Repository Registration
            GenericExtension.RegisterServices(services);

            // Master Role Repositorty Registration
            MasterRoleExtension.RegisterServices(services);

            // User Repository Registration
            UserExtension.RegisterServices(services);
        }
    }
}
