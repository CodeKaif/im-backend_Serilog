using IM.CacheService.V1.Interface;
using IM.Domain.Entities.LayoutDomain;
using IM.Repository.V1.LayoutRepository.Interface;
using IM.Service.V1.Guest.LayoutService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.LayoutService.Impl
{
    public class LayoutGuestSrvc : ILayoutGuestSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly ILayoutRepo _layoutRepo;
        private readonly ICacheSrvc _cacheSrvc;
        public LayoutGuestSrvc(ILayoutRepo layoutRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _layoutRepo = layoutRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Get in cacheing
            var cacheData = await _cacheSrvc.GetAsync<Layout>($"layout_{data.lang}");
            if (cacheData != null)
            {
                return _responseHelper.CreateResponse("Success", cacheData, 200, data.lang, false);
            }
            // Fetch the record using repository
            var layout = await _layoutRepo.GetFirstOrDefault(x => x.lyt_lang == data.lang && x.lyt_is_active == true);
            // If the record is not found, return a proper response
            if (layout == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Set in cacheing
            await _cacheSrvc.SetAsync($"layout_{data.lang}", layout);
            return _responseHelper.CreateResponse("Success", layout, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
