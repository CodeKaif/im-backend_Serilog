using IM.CacheService.V1.Interface;
using IM.Domain.Entities.TermConditionsDomain;
using IM.Repository.V1.TermConditionsRepository.Interface;
using IM.Service.V1.Guest.TermConditionsService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.TermConditionsService.Impl
{
    public class TermConditionsGuestSrvc : ITermConditionsGuestSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly ITermConditionsRepo _termConditionsRepo;
        private readonly ICacheSrvc _cacheSrvc;
        public TermConditionsGuestSrvc(ITermConditionsRepo termConditionsRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _termConditionsRepo = termConditionsRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Get in cacheing
            var cacheData = await _cacheSrvc.GetAsync<TermConditions>($"term_conditions_{data.lang}");
            if (cacheData != null)
            {
                return _responseHelper.CreateResponse("Success", cacheData, 200, data.lang, false);
            }

            // Fetch the record using repository
            var termConditions = await _termConditionsRepo.GetFirstOrDefault(x => x.tc_lang == data.lang && x.tc_is_active == true);
            // If the record is not found, return a proper response
            if (termConditions == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            // Set in cacheing
            await _cacheSrvc.SetAsync($"term_conditions_{data.lang}", termConditions);
            return _responseHelper.CreateResponse("Success", termConditions, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
