using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Dto.V1.InsuranceCompany.Request;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.InsuranceCompanyService.Interface
{
    public interface IInsuranceCompanySrvc
    {
        Task<CommonResponse> GetByIdAsync(int ic_id, RequestMetadata data);
        Task<CommonResponse> LookupAsync(RequestMetadata data);
        Task<CommonResponse> UpdateAsync(InsuranceCompanyUpdateModel request, RequestMetadata data);
        Task<CommonResponse> AddAsync(InsuranceCompanyAddModel request, RequestMetadata data);
        Task<CommonResponse> DeleteAsync(int ic_id, RequestMetadata data);
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
        Task<InsuranceCompany> GetInsuranceCompanyAsync(int ic_id, RequestMetadata data);
    }
}
