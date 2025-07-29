using IM.Domain.Entities.CarCompanyDomain;
using IM.Dto.V1.CarCompany.DataModel;
using IM.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.CarCompanyRepository.Interface
{
    public interface ICarCompanyRepo : IGenericRepositoryAsync<CarCompany>
    {
        Task<(IReadOnlyList<CarCompanyDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<CarCompanyDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
