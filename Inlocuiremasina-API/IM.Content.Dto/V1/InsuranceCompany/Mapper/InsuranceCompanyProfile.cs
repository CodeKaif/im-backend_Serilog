using AutoMapper;
using IM.Dto.V1.InsuranceCompany.DataModel;
using IM.Dto.V1.InsuranceCompany.Response;

namespace IM.Dto.V1.InsuranceCompany.Mapper
{
    public class InsuranceCompanyProfile : Profile, IMappingProfileMarker
    {
        public InsuranceCompanyProfile()
        {
            CreateMap<IM.Domain.Entities.InsuranceCompanyDomain.InsuranceCompany, InsuranceCompanyLookupModel>();
            CreateMap<IM.Domain.Entities.InsuranceCompanyDomain.InsuranceCompany, InsuranceCompanyDto>();
        }
    }

}
