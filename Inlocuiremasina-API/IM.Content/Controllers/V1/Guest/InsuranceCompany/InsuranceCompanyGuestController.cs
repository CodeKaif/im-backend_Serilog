using IM.Service.V1.Guest.InsuranceCompanyService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.InsuranceCompany
{
    [Route("api/v1/guest/insurancecompany")]
    public class InsuranceCompanyGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IInsuranceCompanyGuestSrvc _insuranceCompanyGuestService;
        public InsuranceCompanyGuestController(RequestMetadata metadata, IInsuranceCompanyGuestSrvc insuranceCompanyGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _insuranceCompanyGuestService = insuranceCompanyGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _insuranceCompanyGuestService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
