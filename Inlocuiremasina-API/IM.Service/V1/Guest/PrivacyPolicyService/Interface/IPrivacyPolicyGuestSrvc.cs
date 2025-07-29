using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.PrivacyPolicyService.Interface
{
    public interface IPrivacyPolicyGuestSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
    }
}
