using Auth.Data.Context;
using Auth.Domain.Entities.MasterRoleDomain;
using Auth.Dto.V1.AdminModel.DataModel;
using Auth.Repository.Core.Generic.Impl;
using Auth.Repository.V1.MasterRoleRepositorey.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace Auth.Repository.V1.MasterRoleRepositorey.Impl
{
    public class MasterRoleRepo : GenericRepositoryAsync<MasterRole>, IMasterRoleRepo
    {
        private readonly DbSet<MasterRole> _masterRole;
        private readonly IMapper _mapper;
        public MasterRoleRepo(IMAuthDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _masterRole = dbContext.Set<MasterRole>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<MasterRoleDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _masterRole.ProjectTo<MasterRoleDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _masterRole.ProjectTo<MasterRoleDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<MasterRoleDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _masterRole.ProjectTo<MasterRoleDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _masterRole.ProjectTo<MasterRoleDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
