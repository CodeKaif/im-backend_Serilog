using IM.Domain.Entities.CarCompanyDomain;
using IM.Dto.V1.CarCompany.Request;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.CarCompanyService.Interface
{
    public interface ICarCompanySrvc
    {
        Task<CommonResponse> GetByIdAsync(int cc_id, RequestMetadata data);
        Task<CommonResponse> LookupAsync(RequestMetadata data);
        Task<CommonResponse> UpdateAsync(CarCompanyUpdateModel request, RequestMetadata data);
        Task<CommonResponse> AddAsync(CarCompanyAddModel request, RequestMetadata data);
        Task<CommonResponse> DeleteAsync(int cc_id, RequestMetadata data);
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ReadExcelFile(IFormFile file, RequestMetadata data);
        Task<CarCompany> GetFirstOrDefault(int cc_id, RequestMetadata data);
        Task<List<CarCompany>> GetAllAsync(RequestMetadata data);
    }
}
