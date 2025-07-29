using IM.Data.Context;
using IM.Domain.Entities.TermConditionsDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.TermConditionsRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.TermConditionsRepository.Impl
{
    public class TermConditionsRepo : GenericRepositoryAsync<TermConditions>, ITermConditionsRepo
    {
        private readonly DbSet<TermConditions> _termConditions;
        public TermConditionsRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _termConditions = dbContext.Set<TermConditions>();
        }
    }
}
