using AutoMapper;
using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Dto.V1.InsuranceCompany.Request;
using IM.Dto.V1.InsuranceCompany.Response;
using IM.Repository.V1.InsuranceCompanyRepository.Interface;
using IM.Service.V1.Admin.InsuranceCompanyService.Helper;
using IM.Service.V1.Admin.InsuranceCompanyService.Interface;
using IM.Service.V1.Admin.RequestedReplacementCarService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.InsuranceCompanyService.Impl
{
    public class InsuranceCompanySrvc : IInsuranceCompanySrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly IInsuranceCompanyRepo _insuranceCompanyRepo;
        private readonly InsuranceCompanyHelper _insuranceCompanyHelper;
        private readonly IRequestedReplacementCarSrvc _requestedReplacementCarSrvc;
        public InsuranceCompanySrvc(IMapper mapper, IInsuranceCompanyRepo insuranceCompanyRepo, IRequestedReplacementCarSrvc requestedReplacementCarSrvc, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _insuranceCompanyRepo = insuranceCompanyRepo;
            _responseHelper = responseHelper;
            _insuranceCompanyHelper = new InsuranceCompanyHelper();
            _requestedReplacementCarSrvc = requestedReplacementCarSrvc;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetByIdAsync(int ic_id, RequestMetadata data)
        {
            var insuranceCompany = await _insuranceCompanyRepo.GetFirstOrDefault(x => x.ic_id == ic_id && x.ic_lang == data.lang && x.ic_is_active);
            if (insuranceCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }
            return _responseHelper.CreateResponse("Success", insuranceCompany, 200, data.lang, false);
        }

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
        public async Task<CommonResponse> AddAsync(InsuranceCompanyAddModel request, RequestMetadata data)
        {
            try
            {
                var insuranceCompany = await _insuranceCompanyRepo.GetFirstOrDefault(x => x.ic_lang == data.lang && x.ic_name == request.ic_name && x.ic_is_active);
                if (insuranceCompany != null)
                {
                    return _responseHelper.CreateResponse("RecordAlreadyExist", null, 400, data.lang, false);
                }

                // Add and Mapp Data
                var responce = await _insuranceCompanyRepo.AddAsync(_insuranceCompanyHelper.MapInsuranceCompany(request, data));

                // Return success response
                return _responseHelper.CreateResponse("AddSuccessful", responce, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CommonResponse> UpdateAsync(InsuranceCompanyUpdateModel request, RequestMetadata data)
        {
            // Fetch the record using repository
            var insuranceCompany = await _insuranceCompanyRepo.GetByIdAsync(request.ic_id);

            // If the record is not found, return a proper response
            if (insuranceCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            if(insuranceCompany.ic_name != request.ic_name)
            {
               await _requestedReplacementCarSrvc.UpdateInsuranceCompanyAsync(request.ic_id, request.ic_name, data);
            }

            insuranceCompany.ic_lang = data.lang;
            insuranceCompany.ic_name = request.ic_name;
            insuranceCompany.ic_image = request.ic_image;
            insuranceCompany.ic_desc = request.ic_desc;
            insuranceCompany.ic_updated_at = DateTime.UtcNow;
            insuranceCompany.ic_updated_by = data.userId;

            // Save changes using the repository update method
            await _insuranceCompanyRepo.UpdateAsync(insuranceCompany);

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _insuranceCompanyRepo.PagingAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("PaginatedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _insuranceCompanyRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> DeleteAsync(int ic_id, RequestMetadata data)
        {
            var insuranceCompany = await _insuranceCompanyRepo.GetByIdAsync(ic_id);

            // If the record is not found, return a proper response
            if (insuranceCompany == null || !insuranceCompany.ic_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            insuranceCompany.ic_is_active = false;
            insuranceCompany.ic_updated_at = DateTime.UtcNow;
            insuranceCompany.ic_updated_by = data.userId;

            // Save changes using the repository update method
            await _insuranceCompanyRepo.UpdateAsync(insuranceCompany);

            // Return success response
            return _responseHelper.CreateResponse("DeleteSuccessful", null, 200, data.lang, false);
        }
        #endregion For Admin EndPoint
        #region For Internal Use
        public async Task<InsuranceCompany> GetInsuranceCompanyAsync(int ic_id, RequestMetadata data)
        {
            return await _insuranceCompanyRepo.GetFirstOrDefault(x=> x.ic_id == ic_id && x.ic_lang == data.lang && x.ic_is_active);
        }
        #endregion For Internal Use
    }
}
