using IM.Service.V1.Admin.RequestedReplacementCarService.Extension;
using IM.Service.V1.Admin.CarCompanyService.Extension;
using IM.Service.V1.Admin.CarService.Extension;
using IM.Service.V1.Admin.EmailRecipientService.Extension;
using IM.Service.V1.Admin.HomePageService.Extension;
using IM.Service.V1.Admin.InsuranceCompanyService.Extension;
using IM.Service.V1.Admin.LayoutService.Extension;
using IM.Service.V1.Admin.PickupLocationService.Extension;
using IM.Service.V1.Admin.PrivacyPolicyService.Extension;
using IM.Service.V1.Admin.RequestCarService.Extension;
using IM.Service.V1.Admin.TermConditionsService.Extension;
using IM.Service.V1.Guest.CarCompanyService.Extension;
using IM.Service.V1.Guest.CarService.Extension;
using IM.Service.V1.Guest.HomePageService.Extension;
using IM.Service.V1.Guest.InsuranceCompanyService.Extension;
using IM.Service.V1.Guest.LayoutService.Extension;
using IM.Service.V1.Guest.PickupLocationService.Extension;
using IM.Service.V1.Guest.PrivacyPolicyService.Extension;
using IM.Service.V1.Guest.RequestCarService.Extension;
using IM.Service.V1.Guest.RequestedReplacementCarService.Extension;
using IM.Service.V1.Guest.TermConditionsService.Extension;
using Microsoft.Extensions.DependencyInjection;
using IM.Service.V1.Guest.CountryMasterService.Extension;

namespace IM.Service
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Admin Service Registration
            // Car Service Registration
            CarSrvcExtension.RegisterServices(services);

            // RequestCar Service Registration
            RequestCarSrvcExtension.RegisterServices(services);

            // HomePage Service Registration
            HomePageSrvcExtension.RegisterServices(services);

            // Layout Service Registration
            LayoutSrvcExtension.RegisterServices(services);

            // Term Conditions Service Registration
            TermConditionsSrvcExtension.RegisterServices(services);

            // Privacy Policy Service Registration
            PrivacyPolicySrvcExtension.RegisterServices(services);

            // Pickup Location Service Registration
            PickupLocationSrvcExtension.RegisterServices(services);

            // Car Company Service Registration
            CarCompanySrvcExtension.RegisterServices(services);

            // Insurance Company Service Registration
            InsuranceCompanySrvcExtension.RegisterServices(services);

            // Email Service Registration
            EmailRecipientSrvcExtension.RegisterServices(services);

            // Requested Replacement Car Service Reqistration
            RequestedReplacementCarSrvcExtension.RegisterServices(services);
            #endregion

            #region Guest Service Registration
            // RequestCar Guest Service Registration
            RequestCarGuestSrvcExtension.RegisterServices(services);

            // HomePage Guest Service Registration
            HomePageGuestSrvcExtension.RegisterServices(services);

            // Layout Guest Service Registration
            LayoutGuestSrvcExtension.RegisterServices(services);

            // Term Conditions Guest Service Registration
            TermConditionsGuestSrvcExtension.RegisterServices(services);

            // Privacy Policy Guest Service Registration
            PrivacyPolicyGuestSrvcExtension.RegisterServices(services);

            // Pickup Location Guest Service Registration
            PickupLocationGuestSrvcExtension.RegisterServices(services);

            // Car Company Guest Service Registration
            CarCompanyGuestSrvcExtension.RegisterServices(services);

            // Insurance Company Guest Service Registration
            InsuranceCompanyGuestSrvcExtension.RegisterServices(services);

            // Car Guest Service Registration
            CarGuestSrvcExtension.RegisterServices(services);

            // Requested Replacement Car Guest Service Registration
            RequestedReplacementCarGuestSrvcExtension.RegisterServices(services);

            // Country Master Guest Service Registration
            CountryMasterGuestSrvcExtension.RegisterServices(services);
            #endregion
        }
    }
}
