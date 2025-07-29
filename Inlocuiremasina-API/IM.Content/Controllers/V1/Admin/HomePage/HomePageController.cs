using IM.Dto.V1.UpdateEntity;
using IM.Service.V1.Admin.HomePageService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.HomePage
{
    [ApiController]
    public class HomePageController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IHomePageSrvc _homePageService;
        public HomePageController(RequestMetadata metadata, IHomePageSrvc homePageService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _homePageService = homePageService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _homePageService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEntity request)
        {
            var response = await _homePageService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut("Test-Route")]
        public async Task<IActionResult> TestAsync([FromBody] UpdateEntity request)
        {
            var response = await _homePageService.TestAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
