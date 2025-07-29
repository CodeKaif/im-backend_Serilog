using Auth.Domain.Entities.ApplicationUserDomain;
using Auth.Domain.Entities.MasterRoleDomain;
using Auth.Dto.V1.AdminModel.DataModel;
using Auth.Dto.V1.AdminModel.Response;
using AutoMapper;

namespace Auth.Dto.V1.AdminModel.Mapper
{
    public class AdminProfile : Profile, IMappingProfileMarker
    {
        public AdminProfile()
        {
            CreateMap<MasterRole, MasterRoleDto>();
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUser, AdminGetModel>();
            CreateMap<MasterRole, MasterRoleLookupModel>();
        }
    }
}
