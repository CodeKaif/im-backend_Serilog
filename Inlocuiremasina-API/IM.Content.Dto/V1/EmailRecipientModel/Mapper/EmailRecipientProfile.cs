using AutoMapper;
using IM.Domain.Entities.EmailRecipientDomain;
using IM.Dto.V1.EmailRecipientModel.DataModel;

namespace IM.Dto.V1.EmailRecipientModel.Mapper
{
    public class EmailRecipientProfile : Profile, IMappingProfileMarker
    {
        public EmailRecipientProfile()
        {
            CreateMap<EmailRecipient, EmailRecipientDto>();
        }
    }
}
