using IM.Data.Context;
using IM.Domain.Entities.LayoutDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.LayoutRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.LayoutRepository.Impl
{
    public class LayoutRepo : GenericRepositoryAsync<Layout>, ILayoutRepo
    {
        private readonly DbSet<Layout> _layout;
        public LayoutRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _layout = dbContext.Set<Layout>();
        }
    }
}
