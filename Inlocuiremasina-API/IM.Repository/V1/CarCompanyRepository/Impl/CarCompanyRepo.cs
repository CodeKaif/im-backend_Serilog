using AutoMapper;
using AutoMapper.QueryableExtensions;
using IM.Data.Context;
using IM.Domain.Entities.CarCompanyDomain;
using IM.Dto.V1.CarCompany.DataModel;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.CarCompanyRepository.Interface;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.CarCompanyRepository.Impl
{
    public class CarCompanyRepo : GenericRepositoryAsync<CarCompany>, ICarCompanyRepo
    {
        private readonly DbSet<CarCompany> _carCompany;
        private readonly IMapper _mapper;
        public CarCompanyRepo(IMContentDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _carCompany = dbContext.Set<CarCompany>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<CarCompanyDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _carCompany.Where(a => a.cc_lang == metadata.lang)
                                    .ProjectTo<CarCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _carCompany.Where(a => a.cc_lang == metadata.lang)
                                    .ProjectTo<CarCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }

        public async Task<(IReadOnlyList<CarCompanyDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _carCompany.Where(a => a.cc_lang == metadata.lang)
                                    .ProjectTo<CarCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _carCompany.Where(a => a.cc_lang == metadata.lang)
                                    .ProjectTo<CarCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
