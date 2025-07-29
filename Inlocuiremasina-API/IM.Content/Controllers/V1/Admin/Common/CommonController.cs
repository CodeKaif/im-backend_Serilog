using FileUploader.V1.FileUploadSrvc.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.Common
{
    [ApiController]
    public class CommonController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IFileUploadService _fileUploadService;

        public CommonController(RequestMetadata metadata,
                                ResponseHelper responseHelper,
                                IFileUploadService fileUploadService)
        {
            _metadata = metadata;
            _responseHelper = responseHelper;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("FileUpload/{directory?}")]
        public async Task<IActionResult> Post([FromForm(Name = "file")] IFormFile file, string directory = null)
        {
            try
            {
                string finalDirectory = string.IsNullOrWhiteSpace(directory)
                                           ? _metadata.lang
                                           : $"{_metadata.lang}/{directory}";

                var response = await _fileUploadService.UploadFileAsync(file, finalDirectory);

                var commonResponse = response.success
                                    ? _responseHelper.CreateResponse("UploadSuccess", new { response.ImageUrl }, 200, _metadata.lang, false)
                                    : _responseHelper.CreateResponse("UploadFailed", null, 400, _metadata.lang, false);

                return _responseHelper.ResponseWrapper(commonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
