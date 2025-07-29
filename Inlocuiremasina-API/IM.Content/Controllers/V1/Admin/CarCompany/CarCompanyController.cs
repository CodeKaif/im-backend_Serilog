using IM.Dto.V1.CarCompany.Request;
using IM.Service.V1.Admin.CarCompanyService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.CarCompany
{
    [ApiController]
    public class CarCompanyController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarCompanySrvc _carCompanyService;
        public CarCompanyController(RequestMetadata metadata, ICarCompanySrvc carCompanyService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _carCompanyService = carCompanyService;
            _responseHelper = responseHelper;
        }

        [HttpGet("{cc_id}")]
        public async Task<IActionResult> GetByIdAsync(int cc_id)
        {
            var response = await _carCompanyService.GetByIdAsync(cc_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _carCompanyService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CarCompanyAddModel request)
        {
            var response = await _carCompanyService.AddAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CarCompanyUpdateModel request)
        {
            var response = await _carCompanyService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
            var response = await _carCompanyService.PagingAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("import")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }
            var response = await _carCompanyService.ReadExcelFile(file, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
            var response = await _carCompanyService.ExportAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpDelete("{cc_id}")]
        public async Task<IActionResult> DeleteAsync(int cc_id)
        {
            var response = await _carCompanyService.DeleteAsync(cc_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
