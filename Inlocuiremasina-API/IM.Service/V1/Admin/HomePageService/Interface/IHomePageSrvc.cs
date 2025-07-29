using IM.Dto.V1.UpdateEntity;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.HomePageService.Interface
{
    public interface IHomePageSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
        Task<CommonResponse> UpdateAsync(UpdateEntity request, RequestMetadata data);

        Task<CommonResponse> TestAsync(UpdateEntity request, RequestMetadata data);
    }
}
