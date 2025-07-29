using IM.Dto.V1.PickupLocation.Request;
using IM.Service.V1.Admin.PickupLocationService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.PickupLocation
{
    [ApiController]
    public class PickupLocationController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IPickupLocationSrvc _pickupLocationService;
        public PickupLocationController(RequestMetadata metadata, IPickupLocationSrvc pickupLocationService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _pickupLocationService = pickupLocationService;
            _responseHelper = responseHelper;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _pickupLocationService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpGet("{pl_id}")]
        public async Task<IActionResult> GetByIdAsync(int pl_id)
        {
            var response = await _pickupLocationService.GetByIdAsync(pl_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PickupLocationAddModel request)
        {
            var response = await _pickupLocationService.AddAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PickupLocationUpdateModel request)
        {
            var response = await _pickupLocationService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
            var response = await _pickupLocationService.PagingAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("import")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            // Insert & Save all records
            var response = await _pickupLocationService.ReadExcelFile(file, _metadata);

            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
            var response = await _pickupLocationService.ExportAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpDelete("{pl_id}")]
        public async Task<IActionResult> DeleteAsync( int pl_id)
        {
            var response = await _pickupLocationService.DeleteAsync(pl_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
