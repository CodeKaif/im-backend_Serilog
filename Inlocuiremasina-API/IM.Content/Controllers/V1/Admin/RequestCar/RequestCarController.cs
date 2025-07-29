using IM.Dto.V1.UpdateEntity;
using IM.Service.V1.Admin.RequestCarService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.RequestCar
{
    [ApiController]
    public class RequestCarController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestCarSrvc _requestCarService;
        public RequestCarController(RequestMetadata metadata, IRequestCarSrvc requestCarService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _requestCarService = requestCarService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _requestCarService.GetAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEntity request)
        {
            var response = await _requestCarService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
