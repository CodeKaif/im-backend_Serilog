using IM.CacheService.V1.Interface;
using IM.Dto.V1.UpdateEntity;
using IM.Repository.V1.LayoutRepository.Interface;
using IM.Service.V1.Admin.LayoutService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.LayoutService.Impl
{
    public class LayoutSrvc : ILayoutSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly ILayoutRepo _layoutRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public LayoutSrvc(ILayoutRepo layoutRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _layoutRepo = layoutRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var layout = await _layoutRepo.GetFirstOrDefault(x => x.lyt_lang == data.lang && x.lyt_is_active == true);
            // If the record is not found, return a proper response
            if (layout == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            return _responseHelper.CreateResponse("Success", layout, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAsync(UpdateEntity request, RequestMetadata data)
        {
            // Fetch the record using repository
            var layout = await _layoutRepo.GetByIdAsync(request.Id);

            // If the record is not found, return a proper response
            if (layout == null || !layout.lyt_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            // Validate if the requested property exists in the entity
            var property = layout.GetType().GetProperty(request.Type);
            if (property == null)
            {
                return _responseHelper.CreateResponse("TypeNotValid", null, 400, data.lang, false);
            }

            property.SetValue(layout, request.Value.ToString());

            // Save changes using the repository update method
            await _layoutRepo.UpdateAsync(layout);

            // Remove from cacheing
            await _cacheSrvc.RemoveAsync($"layout_{data.lang}");

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }
        #endregion For Admin EndPoint

    }
}
