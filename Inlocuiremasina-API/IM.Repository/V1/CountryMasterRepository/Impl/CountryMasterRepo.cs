using IM.Data.Context;
using IM.Domain.Entities.CountryMasterDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.CountryMasterRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.CountryMasterRepository.Impl
{
    public class CountryMasterRepo : GenericRepositoryAsync<CountryMaster>, ICountryMasterRepo
    {
        private readonly DbSet<CountryMaster> _countryMaster;
        public CountryMasterRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _countryMaster = dbContext.Set<CountryMaster>();
        }
    }
}
