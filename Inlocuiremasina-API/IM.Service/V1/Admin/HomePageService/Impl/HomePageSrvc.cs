using IM.CacheService.V1.Interface;
using IM.Dto.V1.UpdateEntity;
using IM.Repository.V1.HomePageRepository.Interface;
using IM.Service.V1.Admin.HomePageService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.HomePageService.Impl
{
    public class HomePageSrvc : IHomePageSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IHomePageRepo _homePageRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public HomePageSrvc(IHomePageRepo homePageRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _homePageRepo = homePageRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var homePage = await _homePageRepo.GetFirstOrDefault(x => x.hp_lang == data.lang && x.hp_is_active == true);
            // If the record is not found, return a proper response
            if (homePage == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            return _responseHelper.CreateResponse("Success", homePage, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAsync(UpdateEntity request, RequestMetadata data)
        {
            // Fetch the record using repository
            var homePage = await _homePageRepo.GetFirstOrDefault(x => x.hp_lang == data.lang && x.hp_id == request.Id && x.hp_is_active == true);

            // If the record is not found, return a proper response
            if (homePage == null || !homePage.hp_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Validate if the requested property exists in the entity
            var property = homePage.GetType().GetProperty(request.Type);
            if (property == null)
            {
                return _responseHelper.CreateResponse("TypeNotValid", null, 400, data.lang, false);
            }

            property.SetValue(homePage, request.Value.ToString());

            // Save changes using the repository update method
            await _homePageRepo.UpdateAsync(homePage);

            // Remove from cacheing
            await _cacheSrvc.RemoveAsync($"homepage_{data.lang}");

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }
        #endregion For Admin EndPoint


        public async Task<CommonResponse> TestAsync(UpdateEntity request, RequestMetadata data)
        {
            int a = 500;
            int b = 0;

            int c = a / b;

            return _responseHelper.CreateResponse("UpdateSuccessful", c, 200, data.lang, false);
        }

    }
}
