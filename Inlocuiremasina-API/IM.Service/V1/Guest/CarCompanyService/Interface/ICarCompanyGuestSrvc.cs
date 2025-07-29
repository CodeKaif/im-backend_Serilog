using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.CarCompanyService.Interface
{
    public interface ICarCompanyGuestSrvc
    {
        Task<CommonResponse> LookupAsync(RequestMetadata data);
    }
}
