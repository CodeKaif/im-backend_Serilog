using IM.Service.V1.Guest.RequestCarService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.RequestCar
{
    [Route("api/v1/guest/requestcar")]
    public class RequestCarGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestCarGuestSrvc _requestCarGuestService;
        public RequestCarGuestController(RequestMetadata metadata, IRequestCarGuestSrvc requestCarGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _requestCarGuestService = requestCarGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _requestCarGuestService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
