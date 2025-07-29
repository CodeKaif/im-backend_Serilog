using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.CountryMasterService.Interface
{
    public interface ICountryMasterGuestSrvc
    {
        Task<CommonResponse> LookupAsync(RequestMetadata data);
    }
}
