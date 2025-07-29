using AutoMapper;
using IM.Dto.V1.Car.DataModel;
using IM.Dto.V1.Car.Response;

namespace IM.Dto.V1.Car.Mapper
{
    public class CarProfile : Profile, IMappingProfileMarker
    {
        public CarProfile()
        {
            CreateMap<IM.Domain.Entities.CarDomain.Car, CarLookupModel>();
            CreateMap<IM.Domain.Entities.CarDomain.Car, CarDto>();
        }
    }

}
