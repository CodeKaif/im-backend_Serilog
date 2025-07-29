using IM.Dto.V1.Car.Request;
using IM.Service.V1.Admin.CarService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.CarCompany
{
    [ApiController]
    public class CarController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarSrvc _carService;
        public CarController(RequestMetadata metadata, ICarSrvc carService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _carService = carService;
            _responseHelper = responseHelper;
        }

        [HttpGet("{cr_id}")]
        public async Task<IActionResult> GetByIdAsync(int cr_id)
        {
            var response = await _carService.GetByIdAsync(cr_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("lookup")]
        public async Task<IActionResult> LookupAsync(CarLookupRequest request)
        {
            var response = await _carService.LookupAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CarAddModel request)
        {
            var response = await _carService.AddAsync(request, _metadata);
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
            var response = await _carService.ReadExcelFile(file, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CarUpdateModel request)
        {
            var response = await _carService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
            var response = await _carService.PagingAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }


        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
            var response = await _carService.ExportAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
          
        }

        [HttpDelete("{cr_id}")]
        public async Task<IActionResult> DeleteAsync(int cr_id)
        {
            var response = await _carService.DeleteAsync(cr_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
