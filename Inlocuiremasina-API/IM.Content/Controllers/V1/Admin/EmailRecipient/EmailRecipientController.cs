using IM.Dto.V1.EmailRecipientModel.Request;
using IM.Service.V1.Admin.EmailRecipientService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.EmailRecipient
{
    [ApiController]
    public class EmailRecipientController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IEmailRecipientSrvc _emailRecipientService;
        public EmailRecipientController(RequestMetadata metadata,
                                        IEmailRecipientSrvc emailRecipientService,
                                        ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _emailRecipientService = emailRecipientService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _emailRecipientService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmailRecipientRequest request)
        {
            var response = await _emailRecipientService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
