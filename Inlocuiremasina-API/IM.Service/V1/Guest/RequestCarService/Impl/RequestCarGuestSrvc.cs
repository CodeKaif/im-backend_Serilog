using IM.CacheService.V1.Interface;
using IM.Domain.Entities.LayoutDomain;
using IM.Domain.Entities.RequestCarDomain;
using IM.Repository.V1.RequestCarRepository.Interface;
using IM.Service.V1.Guest.RequestCarService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.RequestCarService.Impl
{
    public class RequestCarGuestSrvc : IRequestCarGuestSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestCarRepo _requestCarRepo;
        private readonly ICacheSrvc _cacheSrvc;
        public RequestCarGuestSrvc(IRequestCarRepo requestCarRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _requestCarRepo = requestCarRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Get in cacheing
            var cacheData = await _cacheSrvc.GetAsync<RequestCar>($"request_car_{data.lang}");
            if (cacheData != null)
            {
                return _responseHelper.CreateResponse("Success", cacheData, 200, data.lang, false);
            }

            // Fetch the record using repository
            var requestCar = await _requestCarRepo.GetFirstOrDefault(x => x.rc_lang == data.lang && x.rc_is_active == true);
            // If the record is not found, return a proper response
            if (requestCar == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            // Set in cacheing
            await _cacheSrvc.SetAsync($"request_car_{data.lang}", requestCar);
            return _responseHelper.CreateResponse("Success", requestCar, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
