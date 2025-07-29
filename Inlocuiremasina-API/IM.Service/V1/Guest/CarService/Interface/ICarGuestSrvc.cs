using IM.Dto.V1.Car.Request;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.CarService.Interface
{
    public interface ICarGuestSrvc
    {
        Task<CommonResponse> LookupAsync(CarLookupRequest request, RequestMetadata data);
    }
}
