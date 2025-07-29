using EmailGateway.V1.Model;
using ResponseWrapper.V1.Model;

namespace EmailGateway.V1.Interface
{
    public interface IEmailGateway
    {
        Task<CommonResponse> SendEmailAsync(EmailRequest request, EmailConfiguration emailSetting = null);
    }
}
