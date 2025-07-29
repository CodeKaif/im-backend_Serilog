using AutoMapper;
using AutoMapper.QueryableExtensions;
using IM.Data.Context;
using IM.Domain.Entities.CarDomain;
using IM.Dto.V1.Car.DataModel;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.CarRepository.Interface;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.CarRepository.Impl
{
    public class CarRepo : GenericRepositoryAsync<Car>, ICarRepo
    {
        private readonly DbSet<Car> _car;
        private readonly IMapper _mapper;
        public CarRepo(IMContentDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _car = dbContext.Set<Car>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<CarDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _car.Where(a => a.cr_lang == metadata.lang)
                                    .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _car.Where(a => a.cr_lang == metadata.lang)
                                    .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<CarDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _car.Where(a => a.cr_lang == metadata.lang)
                                    .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _car.Where(a => a.cr_lang == metadata.lang)
                                    .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
