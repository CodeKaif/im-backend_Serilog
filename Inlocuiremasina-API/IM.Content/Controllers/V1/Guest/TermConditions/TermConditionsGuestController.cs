using IM.Service.V1.Guest.TermConditionsService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.TermConditions
{
    [Route("api/v1/guest/termconditions")]
    public class TermConditionsGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ITermConditionsGuestSrvc _termConditionsGuestService;
        public TermConditionsGuestController(RequestMetadata metadata, ITermConditionsGuestSrvc termConditionsGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _termConditionsGuestService = termConditionsGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _termConditionsGuestService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
