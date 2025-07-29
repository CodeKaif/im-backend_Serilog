using IM.Dto.V1.UpdateEntity;
using IM.Service.V1.Admin.TermConditionsService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.TermConditions
{
    [ApiController]
    public class TermConditionsController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ITermConditionsSrvc _termConditionsService;
        public TermConditionsController(RequestMetadata metadata, ITermConditionsSrvc termConditionsService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _termConditionsService = termConditionsService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _termConditionsService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEntity request)
        {
            var response = await _termConditionsService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
