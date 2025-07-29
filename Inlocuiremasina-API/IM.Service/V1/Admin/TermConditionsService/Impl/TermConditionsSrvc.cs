using IM.CacheService.V1.Interface;
using IM.Dto.V1.UpdateEntity;
using IM.Repository.V1.TermConditionsRepository.Interface;
using IM.Service.V1.Admin.TermConditionsService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.TermConditionsService.Impl
{
    public class TermConditionsSrvc : ITermConditionsSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly ITermConditionsRepo _termConditionsRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public TermConditionsSrvc(ITermConditionsRepo termConditionsRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _termConditionsRepo = termConditionsRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Admin EndPoint
        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var termConditions = await _termConditionsRepo.GetFirstOrDefault(x => x.tc_lang == data.lang && x.tc_is_active == true);
            // If the record is not found, return a proper response
            if (termConditions == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            return _responseHelper.CreateResponse("Success", termConditions, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAsync(UpdateEntity request, RequestMetadata data)
        {
            // Fetch the record using repository
            var termConditions = await _termConditionsRepo.GetByIdAsync(request.Id);

            // If the record is not found, return a proper response
            if (termConditions == null || !termConditions.tc_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Validate if the requested property exists in the entity
            var property = termConditions.GetType().GetProperty(request.Type);
            if (property == null)
            {
                return _responseHelper.CreateResponse("TypeNotValid", null, 400, data.lang, false);
            }

            property.SetValue(termConditions, request.Value.ToString());

            // Save changes using the repository update method
            await _termConditionsRepo.UpdateAsync(termConditions);

            // Remove from cacheing
            await _cacheSrvc.RemoveAsync($"term_conditions_{data.lang}");

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }
        #endregion For Admin EndPoint
    }
}
