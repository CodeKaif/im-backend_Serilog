using AutoMapper;
using AutoMapper.QueryableExtensions;
using IM.Notification.Data.Context;
using IM.Notification.Domain.Entities.ApplicationLogsDomain;
using IM.Notification.Dto.ApplicationLogs.DataModel;
using IM.Notification.Repository.Core.Generic.Impl;
using IM.Notification.Repository.V1.CarCompanyRepository.Interface;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace IM.Notification.Repository.V1.CarCompanyRepository.Impl
{
    public class ApplicationLogsRepo : GenericRepositoryAsync<ApplicationLogs>, IApplicationLogsRepo
    {
        private readonly DbSet<ApplicationLogs> _applicationLogs;
        private readonly IMapper _mapper;
        public ApplicationLogsRepo(IMNotificationDbCotext dbContext, IMapper mapper) : base(dbContext)
        {
            _applicationLogs = dbContext.Set<ApplicationLogs>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<ApplicationLogsDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _applicationLogs.Where(x=>x.Language == metadata.lang)
                                    .ProjectTo<ApplicationLogsDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _applicationLogs
                                    .ProjectTo<ApplicationLogsDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<ApplicationLogsDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _applicationLogs
                                    .ProjectTo<ApplicationLogsDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _applicationLogs
                                    .ProjectTo<ApplicationLogsDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
