using IM.Service.V1.Guest.LayoutService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.HomegePage
{
    [Route("api/v1/guest/layout")]
    public class LayoutGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ILayoutGuestSrvc _layoutGuestService;
        public LayoutGuestController(RequestMetadata metadata, ILayoutGuestSrvc layoutGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _layoutGuestService = layoutGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _layoutGuestService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
