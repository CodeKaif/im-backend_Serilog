using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.TermConditionsService.Interface
{
    public interface ITermConditionsGuestSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
    }
}
