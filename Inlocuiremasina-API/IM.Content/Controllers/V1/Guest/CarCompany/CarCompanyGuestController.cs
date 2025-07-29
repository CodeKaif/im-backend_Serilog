using IM.Service.V1.Guest.CarCompanyService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.CarCompany
{
    [Route("api/v1/guest/carcompany")]
    public class CarCompanyGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarCompanyGuestSrvc _carCompanyGuestService;
        public CarCompanyGuestController(RequestMetadata metadata, ICarCompanyGuestSrvc carCompanyGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _carCompanyGuestService = carCompanyGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _carCompanyGuestService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
