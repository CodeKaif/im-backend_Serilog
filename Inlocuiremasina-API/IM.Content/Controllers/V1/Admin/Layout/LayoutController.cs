using IM.Dto.V1.UpdateEntity;
using IM.Service.V1.Admin.LayoutService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.Layout
{
    [ApiController]
    public class LayoutController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ILayoutSrvc _layoutService;
        public LayoutController(RequestMetadata metadata, ILayoutSrvc layoutService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _layoutService = layoutService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _layoutService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEntity request)
        {
            var response = await _layoutService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
