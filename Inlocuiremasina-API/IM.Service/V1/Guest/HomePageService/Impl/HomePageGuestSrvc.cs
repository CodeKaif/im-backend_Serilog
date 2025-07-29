using IM.CacheService.V1.Interface;
using IM.Domain.Entities.HomePageDomain;
using IM.Repository.V1.HomePageRepository.Interface;
using IM.Service.V1.Guest.HomePageService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.HomePageService.Impl
{
    public class HomePageGuestSrvc : IHomePageGuestSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IHomePageRepo _homePageRepo;
        private readonly ICacheSrvc _cacheSrvc;
        public HomePageGuestSrvc(IHomePageRepo homePageRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _homePageRepo = homePageRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            var cacheKey = await _cacheSrvc.GetAsync<HomePage>($"homepage_{data.lang}");
            if (cacheKey != null)
            {
                return _responseHelper.CreateResponse("Success", cacheKey, 200, data.lang, false);
            }
            // Fetch the record using repository
            var homePage = await _homePageRepo.GetFirstOrDefault(x => x.hp_lang == data.lang && x.hp_is_active == true);
            // If the record is not found, return a proper response
            if (homePage == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            await _cacheSrvc.SetAsync($"homepage_{data.lang}", homePage);
            return _responseHelper.CreateResponse("Success", homePage, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
