using IM.CacheService.V1.Interface;
using IM.Domain.Entities.PrivacyPolicyDomain;
using IM.Repository.V1.PrivacyPolicyRepository.Interface;
using IM.Service.V1.Guest.PrivacyPolicyService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.PrivacyPolicyService.Impl
{
    public class PrivacyPolicyGuestSrvc : IPrivacyPolicyGuestSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IPrivacyPolicyRepo _privacyPolicyRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public PrivacyPolicyGuestSrvc(IPrivacyPolicyRepo privacyPolicyRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _privacyPolicyRepo = privacyPolicyRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Get in cacheing
            var cacheData = await _cacheSrvc.GetAsync<PrivacyPolicy>($"privacy_policy_{data.lang}");
            if (cacheData != null)
            {
                return _responseHelper.CreateResponse("Success", cacheData, 200, data.lang, false);
            }

            // Fetch the record using repository
            var privacyPolicy = await _privacyPolicyRepo.GetFirstOrDefault(x => x.pp_lang == data.lang && x.pp_is_active == true);
            // If the record is not found, return a proper response
            if (privacyPolicy == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Set in cacheing
            await _cacheSrvc.SetAsync($"privacy_policy_{data.lang}", privacyPolicy);

            return _responseHelper.CreateResponse("Success", privacyPolicy, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
