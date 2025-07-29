using IM.Data.Context;
using IM.Domain.Entities.HomePageDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.HomePageRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.HomePageRepository.Impl
{
    public class HomePageRepo : GenericRepositoryAsync<HomePage>, IHomePageRepo
    {
        private readonly DbSet<HomePage> _homePage;
        public HomePageRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _homePage = dbContext.Set<HomePage>();
        }
    }
}
