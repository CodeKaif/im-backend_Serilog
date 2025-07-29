using Auth.Data.Context;
using Auth.Domain.Entities.ApplicationUserDomain;
using Auth.Dto.V1.AdminModel.DataModel;
using Auth.Repository.Core.Generic.Impl;
using Auth.Repository.V1.UserRepository.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace Auth.Repository.V1.UserRepositorey.Impl
{
    public class UserRepo : GenericRepositoryAsync<ApplicationUser>, IUserRepo
    {
        private readonly DbSet<ApplicationUser> _user;
        private readonly IMapper _mapper;
        public UserRepo(IMAuthDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _user = dbContext.Set<ApplicationUser>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<ApplicationUserDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _user.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _user.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<ApplicationUserDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _user.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _user.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
