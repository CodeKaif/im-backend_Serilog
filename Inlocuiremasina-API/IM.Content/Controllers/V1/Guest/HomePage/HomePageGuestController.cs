using IM.Service.V1.Guest.HomePageService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.HomegePage
{
    [Route("api/v1/guest/homepage")]
    public class HomePageGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IHomePageGuestSrvc _homePageGuestService;
        public HomePageGuestController(RequestMetadata metadata, IHomePageGuestSrvc homePageGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _homePageGuestService = homePageGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _homePageGuestService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
