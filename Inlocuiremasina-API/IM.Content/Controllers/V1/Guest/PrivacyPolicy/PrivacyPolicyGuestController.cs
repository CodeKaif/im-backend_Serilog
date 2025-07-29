using IM.Service.V1.Guest.PrivacyPolicyService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.PrivacyPolicy
{
    [Route("api/v1/guest/privacypolicy")]
    public class PrivacyPolicyGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IPrivacyPolicyGuestSrvc _privacyPolicyGuestService;
        public PrivacyPolicyGuestController(RequestMetadata metadata, IPrivacyPolicyGuestSrvc privacyPolicyGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _privacyPolicyGuestService = privacyPolicyGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _privacyPolicyGuestService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
