using AutoMapper;
using IM.Dto.V1.CountryMasterModel.Response;
using IM.Repository.V1.CountryMasterRepository.Interface;
using IM.Service.V1.Guest.CountryMasterService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.CountryMasterService.Impl
{
    public class CountryMasterGuestSrvc : ICountryMasterGuestSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly ICountryMasterRepo _countryMasterRepo;
        public CountryMasterGuestSrvc(IMapper mapper, ICountryMasterRepo countryMasterRepo, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _countryMasterRepo = countryMasterRepo;
            _responseHelper = responseHelper;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> LookupAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var countryMaster = await _countryMasterRepo.GetByConditionsAsync(x => x.cnt_lang == data.lang && x.cnt_is_active == true);
            // If the record is not found, return a proper response
            if (countryMaster == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<CountryMasterLookupModel>>(countryMaster);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
