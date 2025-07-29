using AutoMapper;
using IM.CacheService.V1.Interface;
using IM.Dto.V1.Car.Request;
using IM.Dto.V1.Car.Response;
using IM.Repository.V1.CarRepository.Interface;
using IM.Service.V1.Guest.CarService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.CarService.Impl
{
    public class CarGuestSrvc : ICarGuestSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarRepo _carRepo;
        private readonly ICacheSrvc _cacheSrvc;

        public CarGuestSrvc(IMapper mapper, ICarRepo carRepo, ResponseHelper responseHelper, ICacheSrvc cacheSrvc)
        {
            _mapper = mapper;
            _carRepo = carRepo;
            _responseHelper = responseHelper;
            _cacheSrvc = cacheSrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> LookupAsync(CarLookupRequest request, RequestMetadata data)
        {
            string cacheKey = $"car_lookup:{data.lang}:{request.cr_model}:{request.cr_fk_cc_name}:{request.cr_name}:{request.cr_is_available}:{request.cr_is_active}";

            var res = await _cacheSrvc.GetAsync<List< CarLookupModel>> (cacheKey);
            if (res != null)
            {
                return _responseHelper.CreateResponse("Success", res, 200, data.lang, false);
            }
            // Fetch the record using repository
            var carList = await _carRepo.GetByConditionsAsync(x => x.cr_lang == data.lang &&
            (request.cr_is_active == null || x.cr_is_active == request.cr_is_active) &&
            (request.cr_is_available == null || x.cr_is_available == request.cr_is_available) &&
            (request.cr_fk_cc_name == "all" || x.cr_fk_cc_name == request.cr_fk_cc_name) &&
            (request.cr_name == "all" || x.cr_name == request.cr_name) &&
            (request.cr_model == "all" || x.cr_model == request.cr_model));
            // If the record is not found, return a proper response
            if (carList == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<CarLookupModel>>(carList);

            await _cacheSrvc.SetAsync(cacheKey, response);
            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
