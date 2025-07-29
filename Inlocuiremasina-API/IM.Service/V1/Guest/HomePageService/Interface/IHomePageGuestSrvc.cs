using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.HomePageService.Interface
{
    public interface IHomePageGuestSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
    }
}
