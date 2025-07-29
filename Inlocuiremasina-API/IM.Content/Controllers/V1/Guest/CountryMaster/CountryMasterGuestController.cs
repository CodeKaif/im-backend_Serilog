using IM.Dto.V1.Car.Request;
using IM.Service.V1.Guest.CarService.Interface;
using IM.Service.V1.Guest.CountryMasterService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Content.Controllers.V1.Guest.Car
{
    [Route("api/v1/guest/countrymaster")]
    public class CountryMasterGuestController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly ICountryMasterGuestSrvc _countryMasterGuestService;
        public CountryMasterGuestController(RequestMetadata metadata, ICountryMasterGuestSrvc countryMasterGuestService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _countryMasterGuestService = countryMasterGuestService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> LookupAsync()
        {
            var response = await _countryMasterGuestService.LookupAsync(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }
    }
}
