using IM.Dto.V1.InsuranceCompany.Request;
using IM.Service.V1.Admin.InsuranceCompanyService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Admin.CarCompany
{
    [ApiController]
    public class InsuranceCompanyController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IInsuranceCompanySrvc _insuranceCompanyService;
        public InsuranceCompanyController(RequestMetadata metadata, IInsuranceCompanySrvc insuranceCompanyService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _insuranceCompanyService = insuranceCompanyService;
            _responseHelper = responseHelper;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _insuranceCompanyService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
        [HttpGet("{ic_id}")]
        public async Task<IActionResult> GetByIdAsync(int ic_id)
        {
            var response = await _insuranceCompanyService.GetByIdAsync(ic_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
       

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] InsuranceCompanyAddModel request)
        {
            var response = await _insuranceCompanyService.AddAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] InsuranceCompanyUpdateModel request)
        {
            var response = await _insuranceCompanyService.UpdateAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
            var response = await _insuranceCompanyService.PagingAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
            var response = await _insuranceCompanyService.ExportAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpDelete("{ic_id}")]
        public async Task<IActionResult> DeleteAsync(int ic_id)
        {
            var response = await _insuranceCompanyService.DeleteAsync(ic_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
