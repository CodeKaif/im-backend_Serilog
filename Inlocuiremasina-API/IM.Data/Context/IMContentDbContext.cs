using IM.Domain.Entities.CarCompanyDomain;
using IM.Domain.Entities.CarDomain;
using IM.Domain.Entities.CountryMasterDomain;
using IM.Domain.Entities.EmailRecipientDomain;
using IM.Domain.Entities.HomePageDomain;
using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Domain.Entities.LayoutDomain;
using IM.Domain.Entities.PickupLocationDomain;
using IM.Domain.Entities.PrivacyPolicyDomain;
using IM.Domain.Entities.RequestCarDomain;
using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Domain.Entities.TermConditionsDomain;
using Microsoft.EntityFrameworkCore;

namespace IM.Data.Context
{
    public class IMContentDbContext : DbContext
    {
        public IMContentDbContext(DbContextOptions<IMContentDbContext> options) : base(options)
        {
        }

        public DbSet<RequestCar> request_car { get; set; }
        public DbSet<HomePage> home_page { get; set; }
        public DbSet<Layout> layout { get; set; }
        public DbSet<TermConditions> term_conditions { get; set; }
        public DbSet<PrivacyPolicy> privacy_policy { get; set; }
        public DbSet<PickupLocation> pickup_location { get; set; }
        public DbSet<CarCompany> car_company { get; set; }
        public DbSet<InsuranceCompany> insurance_company { get; set; }
        public DbSet<Car> car { get; set; }
        public DbSet<EmailRecipient> email_recipient { get; set; }
        public DbSet<RequestedReplacementCar> requested_replacement_car { get; set; }
        public DbSet<CountryMaster> country_master { get; set; }
    }
}
