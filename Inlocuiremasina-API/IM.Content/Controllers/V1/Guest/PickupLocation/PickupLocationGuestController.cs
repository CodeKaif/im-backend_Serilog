using IM.Service.V1.Guest.PickupLocationService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.PickupLocation
{
    [Route("api/v1/guest/pickuplocation")]
    public class PickupLocationGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IPickupLocationGuestSrvc _pickupLocationGuestService;
        public PickupLocationGuestController(RequestMetadata metadata, IPickupLocationGuestSrvc pickupLocationGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _pickupLocationGuestService = pickupLocationGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _pickupLocationGuestService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
