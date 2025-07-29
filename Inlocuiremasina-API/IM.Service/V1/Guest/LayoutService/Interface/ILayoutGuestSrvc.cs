using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.LayoutService.Interface
{
    public interface ILayoutGuestSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
    }
}
