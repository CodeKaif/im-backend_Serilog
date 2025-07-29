using IM.Dto.V1.Car.Request;
using IM.Service.V1.Guest.CarService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.Car
{
    [Route("api/v1/guest/car")]
    public class CarGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarGuestSrvc _carGuestService;
        public CarGuestController(RequestMetadata metadata, ICarGuestSrvc carGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _carGuestService = carGuestService;
            _responseHelper = responseHelper;
        }

        [HttpPost]
        public async Task<IActionResult> LookupAsync(CarLookupRequest request)
        {
            var response = await _carGuestService.LookupAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
