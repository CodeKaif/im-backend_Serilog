using AutoMapper;
using IM.Dto.V1.PickupLocation.Response;
using IM.Repository.V1.PickupLocationRepository.Interface;
using IM.Service.V1.Guest.PickupLocationService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.PickupLocationService.Impl
{
    public class PickupLocationGuestSrvc : IPickupLocationGuestSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly IPickupLocationRepo _pickupLocationRepo;
        public PickupLocationGuestSrvc(IMapper mapper, IPickupLocationRepo pickupLocationRepo, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _pickupLocationRepo = pickupLocationRepo;
            _responseHelper = responseHelper;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> LookupAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var pickupLocation = await _pickupLocationRepo.GetByConditionsAsync(x => x.pl_lang == data.lang && x.pl_is_active == true);
            // If the record is not found, return a proper response
            if (pickupLocation == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<PickupLocationLookupModel>>(pickupLocation);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        #endregion For Guest EndPoint
    }
}
