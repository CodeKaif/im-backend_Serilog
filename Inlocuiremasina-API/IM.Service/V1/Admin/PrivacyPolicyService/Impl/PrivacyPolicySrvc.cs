using IM.CacheService.V1.Interface;
using IM.Dto.V1.UpdateEntity;
using IM.Repository.V1.PrivacyPolicyRepository.Interface;
using IM.Service.V1.Admin.PrivacyPolicyService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.PrivacyPolicyService.Impl
{
    public class PrivacyPolicySrvc : IPrivacyPolicySrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IPrivacyPolicyRepo _privacyPolicyRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public PrivacyPolicySrvc(IPrivacyPolicyRepo privacyPolicyRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _privacyPolicyRepo = privacyPolicyRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var privacyPolicy = await _privacyPolicyRepo.GetFirstOrDefault(x => x.pp_lang == data.lang && x.pp_is_active == true);
            // If the record is not found, return a proper response
            if (privacyPolicy == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            return _responseHelper.CreateResponse("Success", privacyPolicy, 200, data.lang, false);
        }
        public async Task<CommonResponse> UpdateAsync(UpdateEntity request, RequestMetadata data)
        {
            // Fetch the record using repository
            var privacyPolicy = await _privacyPolicyRepo.GetByIdAsync(request.Id);

            // If the record is not found, return a proper response
            if (privacyPolicy == null || !privacyPolicy.pp_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Validate if the requested property exists in the entity
            var property = privacyPolicy.GetType().GetProperty(request.Type);
            if (property == null)
            {
                return _responseHelper.CreateResponse("TypeNotValid", null, 400, data.lang, false);
            }

            property.SetValue(privacyPolicy, request.Value.ToString());

            // Save changes using the repository update method
            await _privacyPolicyRepo.UpdateAsync(privacyPolicy);

            // Remove from cacheing
            await _cacheSrvc.RemoveAsync($"privacy_policy_{data.lang}");
            
            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }
        #endregion For Admin EndPoint

    }
}
