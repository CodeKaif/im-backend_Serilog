using AutoMapper;
using AutoMapper.QueryableExtensions;
using IM.Data.Context;
using IM.Domain.Entities.PickupLocationDomain;
using IM.Dto.V1.PickupLocation.DataModel;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.PickupLocationRepository.Interface;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.PickupLocationRepository.Impl
{
    public class PickupLocationRepo : GenericRepositoryAsync<PickupLocation>, IPickupLocationRepo
    {
        private readonly DbSet<PickupLocation> _pickupLocation;
        private readonly IMapper _mapper;
        public PickupLocationRepo(IMContentDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _pickupLocation = dbContext.Set<PickupLocation>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<PickupLocationDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _pickupLocation.Where(a => a.pl_lang == metadata.lang)
                                    .ProjectTo<PickupLocationDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _pickupLocation.Where(a => a.pl_lang == metadata.lang)
                                    .ProjectTo<PickupLocationDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<PickupLocationDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _pickupLocation.Where(a => a.pl_lang == metadata.lang)
                                    .ProjectTo<PickupLocationDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _pickupLocation.Where(a => a.pl_lang == metadata.lang)
                                    .ProjectTo<PickupLocationDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
