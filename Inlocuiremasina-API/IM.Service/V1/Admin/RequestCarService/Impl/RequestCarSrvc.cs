using IM.CacheService.V1.Interface;
using IM.Dto.V1.UpdateEntity;
using IM.Repository.V1.RequestCarRepository.Interface;
using IM.Service.V1.Admin.RequestCarService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.RequestCarService.Impl
{
    public class RequestCarSrvc : IRequestCarSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestCarRepo _requestCarRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public RequestCarSrvc(IRequestCarRepo requestCarRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _requestCarRepo = requestCarRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }
        #region For Admin EndPoint

        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var requestCar = await _requestCarRepo.GetFirstOrDefault(x => x.rc_lang == data.lang && x.rc_is_active == true);
            // If the record is not found, return a proper response
            if (requestCar == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            return _responseHelper.CreateResponse("Success", requestCar, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAsync(UpdateEntity request, RequestMetadata data)
        {
            // Fetch the record using repository
            var requestCar = await _requestCarRepo.GetByIdAsync(request.Id);

            // If the record is not found, return a proper response
            if (requestCar == null || !requestCar.rc_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Validate if the requested property exists in the entity
            var property = requestCar.GetType().GetProperty(request.Type);
            if (property == null)
            {
                return _responseHelper.CreateResponse("TypeNotValid", null, 400, data.lang, false);
            }

            property.SetValue(requestCar, request.Value.ToString());

            // Save changes using the repository update method
            await _requestCarRepo.UpdateAsync(requestCar);

            // Remove from cacheing
            await _cacheSrvc.RemoveAsync($"request_car_{data.lang}");

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }
        #endregion For Admin EndPoint
    }
}
