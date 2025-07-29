using AutoMapper;
using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.RequestedReplacementCarModel.DataModel;

namespace IM.Dto.V1.RequestedReplacementCarModel.Mapper
{
    public class RequestedReplacementCarProfile : Profile, IMappingProfileMarker
    {
        public RequestedReplacementCarProfile()
        {
            CreateMap<RequestedReplacementCar, RequestedReplacementCarDto>();
        }
    }
}
