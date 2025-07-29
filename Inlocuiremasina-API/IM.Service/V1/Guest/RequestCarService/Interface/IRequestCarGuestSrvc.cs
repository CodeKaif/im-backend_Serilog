using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.RequestCarService.Interface
{
    public interface IRequestCarGuestSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
    }
}
