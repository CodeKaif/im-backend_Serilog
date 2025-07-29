using IM.Dto.V1.RequestedReplacementCarModel.Request;
using IM.Service.V1.Admin.RequestedReplacementCarService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.RequestedReplacementCar
{
    [ApiController]
    public class RequestedReplacementCarController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestedReplacementCarSrvc _requestedReplacementCarService;
        public RequestedReplacementCarController(RequestMetadata metadata, IRequestedReplacementCarSrvc requestedReplacementCarService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _requestedReplacementCarService = requestedReplacementCarService;
            _responseHelper = responseHelper;
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
            var response = await _requestedReplacementCarService.PagingAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
            var response = await _requestedReplacementCarService.ExportAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut("resend/admin")]
        public async Task<IActionResult> ResendAdminMail([FromBody] ResendMailRequest request)
        {
            var response = await _requestedReplacementCarService.ResendAdminMail(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut("resend/user")]
        public async Task<IActionResult> ResendUserMail([FromBody] ResendMailRequest request)
        {
            var response = await _requestedReplacementCarService.ResendUserMail(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
