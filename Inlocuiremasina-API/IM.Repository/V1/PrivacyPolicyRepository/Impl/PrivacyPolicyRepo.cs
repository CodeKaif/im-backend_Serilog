using IM.Data.Context;
using IM.Domain.Entities.PrivacyPolicyDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.PrivacyPolicyRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.PrivacyPolicyRepository.Impl
{
    public class PrivacyPolicyRepo : GenericRepositoryAsync<PrivacyPolicy>, IPrivacyPolicyRepo
    {
        private readonly DbSet<PrivacyPolicy> _privacyPolicy;
        public PrivacyPolicyRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _privacyPolicy = dbContext.Set<PrivacyPolicy>();
        }
    }
}
