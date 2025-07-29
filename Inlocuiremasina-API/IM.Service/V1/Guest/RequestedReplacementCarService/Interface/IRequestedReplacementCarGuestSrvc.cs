using IM.Dto.V1.RequestedReplacementCarModel.Request;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.RequestedReplacementCarService.Interface
{
    public interface IRequestedReplacementCarGuestSrvc
    {
        Task<CommonResponse> AddAsync(AddRequestedReplacementCarRequest request, RequestMetadata data);
    }
}
