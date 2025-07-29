using IM.Dto.V1.UpdateEntity;
using IM.Service.V1.Admin.PrivacyPolicyService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.PrivacyPolicy
{
    [ApiController]
    public class PrivacyPolicyController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IPrivacyPolicySrvc _privacyPolicyService;
        public PrivacyPolicyController(RequestMetadata metadata, IPrivacyPolicySrvc privacyPolicyService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _privacyPolicyService = privacyPolicyService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _privacyPolicyService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEntity request)
        {
            var response = await _privacyPolicyService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
