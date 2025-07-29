using AutoMapper;
using IM.Dto.V1.InsuranceCompany.Response;
using IM.Repository.V1.InsuranceCompanyRepository.Interface;
using IM.Service.V1.Guest.InsuranceCompanyService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.InsuranceCompanyService.Impl
{
    public class InsuranceCompanyGuestSrvc : IInsuranceCompanyGuestSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly IInsuranceCompanyRepo _insuranceCompanyRepo;
        public InsuranceCompanyGuestSrvc(IMapper mapper, IInsuranceCompanyRepo insuranceCompanyRepo, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _insuranceCompanyRepo = insuranceCompanyRepo;
            _responseHelper = responseHelper;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> LookupAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var insuranceCompany = await _insuranceCompanyRepo.GetByConditionsAsync(x => x.ic_lang == data.lang && x.ic_is_active == true);
            // If the record is not found, return a proper response
            if (insuranceCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<InsuranceCompanyLookupModel>>(insuranceCompany);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
