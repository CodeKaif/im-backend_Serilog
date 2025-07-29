using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Dto.V1.InsuranceCompany.DataModel;
using IM.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.InsuranceCompanyRepository.Interface
{
    public interface IInsuranceCompanyRepo : IGenericRepositoryAsync<InsuranceCompany>
    {
        Task<(IReadOnlyList<InsuranceCompanyDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<InsuranceCompanyDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
