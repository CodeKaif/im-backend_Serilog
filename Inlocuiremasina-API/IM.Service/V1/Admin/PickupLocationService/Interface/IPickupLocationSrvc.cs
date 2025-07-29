using IM.Dto.V1.PickupLocation.Request;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.PickupLocationService.Interface
{
    public interface IPickupLocationSrvc
    {
        Task<CommonResponse> GetByIdAsync(int pl_id, RequestMetadata data);
        Task<CommonResponse> LookupAsync(RequestMetadata data);
        Task<CommonResponse> UpdateAsync(PickupLocationUpdateModel request, RequestMetadata data);
        Task<CommonResponse> AddAsync(PickupLocationAddModel request, RequestMetadata data);
        Task<CommonResponse> DeleteAsync(int pl_id, RequestMetadata data);
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ReadExcelFile(IFormFile file, RequestMetadata data);
    }
}
