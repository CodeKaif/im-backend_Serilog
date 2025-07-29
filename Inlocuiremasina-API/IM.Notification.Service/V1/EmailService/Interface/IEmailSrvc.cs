using EmailGateway.V1.Model;
using IM.Notification.Dto.EmailSend;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Notification.Service.V1.EmailService.Interface
{
    public interface IEmailSrvc
    {
        Task<Response<EmailSccessModel>> SendEmailAsync<T>(EmailSendRequest request, RequestMetadata data);
    }
}
