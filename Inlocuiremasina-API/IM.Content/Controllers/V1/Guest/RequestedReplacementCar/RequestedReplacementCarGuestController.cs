using IM.Dto.V1.RequestedReplacementCarModel.Request;
using IM.Service.V1.Guest.RequestedReplacementCarService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.RequestedReplacementCar
{
    [Route("api/v1/guest/requestedreplacementcar")]
    public class RequestedReplacementCarGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestedReplacementCarGuestSrvc _requestedReplacementCarGuestService;
        public RequestedReplacementCarGuestController(RequestMetadata metadata, IRequestedReplacementCarGuestSrvc requestedReplacementCarGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _requestedReplacementCarGuestService = requestedReplacementCarGuestService;
            _responseHelper = responseHelper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddRequestedReplacementCarRequest request)
        {
            var response = await _requestedReplacementCarGuestService.AddAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
