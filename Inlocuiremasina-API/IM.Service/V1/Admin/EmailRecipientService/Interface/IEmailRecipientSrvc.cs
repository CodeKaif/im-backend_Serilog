using IM.Domain.Entities.EmailRecipientDomain;
using IM.Dto.V1.EmailRecipientModel.Request;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.EmailRecipientService.Interface
{
    public interface IEmailRecipientSrvc
    {
        Task<CommonResponse> GetAsync(RequestMetadata data);
        Task<CommonResponse> UpdateAsync(UpdateEmailRecipientRequest request, RequestMetadata data);
        Task<EmailRecipient> GetFirstOrDefault(RequestMetadata data);
    }
}
