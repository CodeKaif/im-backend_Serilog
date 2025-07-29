using AutoMapper;
using IM.Dto.V1.CarCompany.Response;
using IM.Repository.V1.CarCompanyRepository.Interface;
using IM.Service.V1.Guest.CarCompanyService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.CarCompanyService.Impl
{
    public class CarCompanyGuestSrvc : ICarCompanyGuestSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarCompanyRepo _carCompanyRepo;
        public CarCompanyGuestSrvc(IMapper mapper, ICarCompanyRepo carCompanyRepo, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _carCompanyRepo = carCompanyRepo;
            _responseHelper = responseHelper;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> LookupAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var carCompany = await _carCompanyRepo.GetByConditionsAsync(x => x.cc_lang == data.lang && x.cc_is_active == true);
            // If the record is not found, return a proper response
            if (carCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<CarCompanyLookupModel>>(carCompany);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
