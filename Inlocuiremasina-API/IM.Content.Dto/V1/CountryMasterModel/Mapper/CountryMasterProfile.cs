using AutoMapper;
using IM.Domain.Entities.CountryMasterDomain;
using IM.Dto.V1.CountryMasterModel.Response;

namespace IM.Dto.V1.CountryMasterModel.Mapper
{
    public class CountryMasterProfile : Profile, IMappingProfileMarker
    {
        public CountryMasterProfile()
        {
            CreateMap<CountryMaster, CountryMasterLookupModel>();
        }
    }

}
