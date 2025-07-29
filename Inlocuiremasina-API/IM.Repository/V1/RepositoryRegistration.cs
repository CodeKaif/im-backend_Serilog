using IM.Repository.Core.Generic.Extension;
using IM.Repository.V1.CarCompanyRepository.Extension;
using IM.Repository.V1.CarRepository.Extension;
using IM.Repository.V1.CountryMasterRepository.Extension;
using IM.Repository.V1.EmailRecipientRepository.Extension;
using IM.Repository.V1.HomePageRepository.Extension;
using IM.Repository.V1.InsuranceCompanyRepository.Extension;
using IM.Repository.V1.LayoutRepository.Extension;
using IM.Repository.V1.PickupLocationRepository.Extension;
using IM.Repository.V1.PrivacyPolicyRepository.Extension;
using IM.Repository.V1.RequestCarRepository.Extension;
using IM.Repository.V1.RequestedReplacementCarRepository.Extension;
using IM.Repository.V1.TermConditionsRepository.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1
{
    public class RepositoryRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Generic Repository Registration
            GenericExtension.RegisterServices(services);

            // RequestCar Repository Registration
            RequestCarExtension.RegisterServices(services);

            // HomePage Repository Registration
            HomePageExtension.RegisterServices(services);

            // Layout Repository Registration
            LayoutExtension.RegisterServices(services);

            // Term Conditions Repository Registration
            TermConditionsExtension.RegisterServices(services);

            // Privacy Policy Repository Registration
            PrivacyPolicyExtension.RegisterServices(services);

            // Pickup Location Repository Registration
            PickupLocationExtension.RegisterServices(services);

            //Car Company Repository Registration
            CarCompanyExtension.RegisterServices(services);

            // Insurance Company Repository Registration
            InsuranceCompanyExtension.RegisterServices(services);

            // Car Repository Registration
            CarExtension.RegisterServices(services);

            // EmailRecipient Repository Registration
            EmailRecipientExtension.RegisterServices(services);

            // Requested Replacement Car repository registration
            RequestedReplacementCarExtension.RegisterServices(services);

            // Country Master Repository Registration
            CountryMasterExtension.RegisterServices(services);
        }
    }
}
