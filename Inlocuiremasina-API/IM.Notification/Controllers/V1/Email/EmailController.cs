using CommonApiCallHelper.V1.Model;
using EmailGateway.V1.Model;
using IM.Notification.Dto.EmailSend;
using IM.Notification.Service.V1.EmailService.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Notification.Controllers.V1.Email
{
    [ApiController]
    public class EmailController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IEmailSrvc _emailService;
        public EmailController(RequestMetadata metadata, IEmailSrvc emailService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _emailService = emailService;
            _responseHelper = responseHelper;
        }

        [HttpPost("sendmail")]
        public async void SendEmailAsync([FromBody] EmailSendRequest request)
        {
            Task.Run(async () => await _emailService.SendEmailAsync<EmailSccessModel>(request, _metadata));
        }
    }
}
