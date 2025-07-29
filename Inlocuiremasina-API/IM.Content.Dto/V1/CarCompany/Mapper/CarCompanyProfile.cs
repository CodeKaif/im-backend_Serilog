using AutoMapper;
using IM.Dto.V1.CarCompany.DataModel;
using IM.Dto.V1.CarCompany.Response;

namespace IM.Dto.V1.CarCompany.Mapper
{
    public class CarCompanyProfile : Profile, IMappingProfileMarker
    {
        public CarCompanyProfile()
        {
            CreateMap<IM.Domain.Entities.CarCompanyDomain.CarCompany, CarCompanyDto>();
            CreateMap<IM.Domain.Entities.CarCompanyDomain.CarCompany, CarCompanyLookupModel>();
        }
    }

}
