using IM.Notification.Service.V1.ApplicationLogsService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Notification.Controllers.V1.ApplicationLogs
{
    [ApiController]
    public class ApplicationLogsController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IApplicationLogsSrvc _applicationLogsService;
        public ApplicationLogsController(RequestMetadata metadata, IApplicationLogsSrvc applicationLogsService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _applicationLogsService = applicationLogsService;
            _responseHelper = responseHelper;
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
            var response = await _applicationLogsService.PagingAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
          
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
            var response = await _applicationLogsService.ExportAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
