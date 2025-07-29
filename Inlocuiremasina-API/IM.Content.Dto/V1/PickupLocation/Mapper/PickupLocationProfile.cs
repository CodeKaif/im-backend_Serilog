using AutoMapper;
using IM.Dto.V1.PickupLocation.DataModel;
using IM.Dto.V1.PickupLocation.Response;

namespace IM.Dto.V1.PickupLocation.Mapper
{
    public class PickupLocationProfile : Profile
    {
        public PickupLocationProfile()
        {
            CreateMap<Domain.Entities.PickupLocationDomain.PickupLocation, PickupLocationLookupModel>();
            CreateMap<Domain.Entities.PickupLocationDomain.PickupLocation, PickupLocationDto>();
        }
    }

}
