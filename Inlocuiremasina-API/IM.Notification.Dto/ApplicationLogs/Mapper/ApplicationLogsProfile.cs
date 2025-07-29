using AutoMapper;
using IM.Notification.Dto.ApplicationLogs.DataModel;

namespace IM.Notification.Dto.ApplicationLogs.Mapper
{
    class ApplicationLogsProfile : Profile, IMappingProfileMarker
    {
        public ApplicationLogsProfile()
        {
            CreateMap<IM.Notification.Domain.Entities.ApplicationLogsDomain.ApplicationLogs, ApplicationLogsDto>();
        }
    }
}
