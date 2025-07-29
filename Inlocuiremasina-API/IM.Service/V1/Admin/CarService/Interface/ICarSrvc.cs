using IM.Domain.Entities.CarDomain;
using IM.Dto.V1.Car.Request;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.CarService.Interface
{
    public interface     ICarSrvc
    {
        Task<CommonResponse> GetByIdAsync(int cr_id, RequestMetadata data);
        Task<CommonResponse> LookupAsync(CarLookupRequest request, RequestMetadata data);
        Task<CommonResponse> UpdateAsync(CarUpdateModel request, RequestMetadata data);
        Task<CommonResponse> AddAsync(CarAddModel request, RequestMetadata data);
        Task<CommonResponse> DeleteAsync(int cr_id, RequestMetadata data);
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ReadExcelFile(IFormFile file, RequestMetadata data);
        Task<Car> GetByCarCompanyAsync(int cr_fk_cc_id, RequestMetadata data);
        Task<bool> UpdateCarCompanyNameAsync(int cc_id, string companyName, RequestMetadata data);
        Task<Car> GetCarAsync(int cr_id, RequestMetadata data);
    }
}
