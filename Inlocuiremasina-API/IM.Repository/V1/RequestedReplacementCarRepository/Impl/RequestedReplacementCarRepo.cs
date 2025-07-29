using AutoMapper;
using AutoMapper.QueryableExtensions;
using IM.Data.Context;
using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.RequestedReplacementCarModel.DataModel;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.RequestedReplacementCarRepository.Interface;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.RequestedReplacementCarRepository.Impl
{
    public class RequestedReplacementCarRepo : GenericRepositoryAsync<RequestedReplacementCar>, IRequestedReplacementCarRepo
    {
        private readonly DbSet<RequestedReplacementCar> _requestedReplacementCar;
        private readonly IMapper _mapper;
        public RequestedReplacementCarRepo(IMContentDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _requestedReplacementCar = dbContext.Set<RequestedReplacementCar>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<RequestedReplacementCarDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _requestedReplacementCar.Where(a => a.rrc_lang == metadata.lang)
                                    .ProjectTo<RequestedReplacementCarDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _requestedReplacementCar.Where(a => a.rrc_lang == metadata.lang)
                                    .ProjectTo<RequestedReplacementCarDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<RequestedReplacementCarDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _requestedReplacementCar.Where(a => a.rrc_lang == metadata.lang)
                                    .ProjectTo<RequestedReplacementCarDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _requestedReplacementCar.Where(a => a.rrc_lang == metadata.lang)
                                    .ProjectTo<RequestedReplacementCarDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
