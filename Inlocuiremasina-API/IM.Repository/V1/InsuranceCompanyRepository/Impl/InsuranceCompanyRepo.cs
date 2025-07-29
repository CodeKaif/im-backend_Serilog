using AutoMapper;
using AutoMapper.QueryableExtensions;
using IM.Data.Context;
using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Dto.V1.InsuranceCompany.DataModel;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.InsuranceCompanyRepository.Interface;
using Infrastructure.V1.FilterBase;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.EntityFrameworkCore;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.InsuranceCompanyRepository.Impl
{
    public class InsuranceCompanyRepo : GenericRepositoryAsync<InsuranceCompany>, IInsuranceCompanyRepo
    {
        private readonly DbSet<InsuranceCompany> _insuranceCompany;
        private readonly IMapper _mapper;
        public InsuranceCompanyRepo(IMContentDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _insuranceCompany = dbContext.Set<InsuranceCompany>();
            _mapper = mapper;
        }

        public async Task<(IReadOnlyList<InsuranceCompanyDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _insuranceCompany.Where(a => a.ic_lang == metadata.lang)
                                    .ProjectTo<InsuranceCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).ToListAsync();

            var countTask = await _insuranceCompany.Where(a => a.ic_lang == metadata.lang)
                                    .ProjectTo<InsuranceCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToPaggingView(filter).CountAsync();

            return (allTask, countTask);
        }
        public async Task<(IReadOnlyList<InsuranceCompanyDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata)
        {
            var allTask = await _insuranceCompany.Where(a => a.ic_lang == metadata.lang)
                                    .ProjectTo<InsuranceCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).ToListAsync();

            var countTask = await _insuranceCompany.Where(a => a.ic_lang == metadata.lang)
                                    .ProjectTo<InsuranceCompanyDto>(_mapper.ConfigurationProvider)
                                    .ToExportView(filter).CountAsync();

            return (allTask, countTask);
        }
    }
}
