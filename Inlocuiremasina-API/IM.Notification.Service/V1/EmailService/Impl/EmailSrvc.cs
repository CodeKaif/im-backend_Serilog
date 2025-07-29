using EmailGateway.V1.Interface;
using EmailGateway.V1.Model;
using IM.Notification.Dto.EmailSend;
using IM.Notification.Service.V1.EmailService.Interface;
using Microsoft.Extensions.Options;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;
using System.Text;
using System.Threading.Tasks;

namespace IM.Notification.Service.V1.EmailService.Impl
{
    public class EmailSrvc: IEmailSrvc
    {
        private readonly IOptions<EmailConfiguration> _emailConfig;
        private readonly ResponseHelper _responseHelper;
        private readonly IEmailGateway _iEmailGateway;

        public EmailSrvc(IEmailGateway iEmailGateway, ResponseHelper responseHelper, IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig;
            _iEmailGateway = iEmailGateway;
            _responseHelper = responseHelper;
        }
        public async Task<Response<EmailSccessModel>> SendEmailAsync<T>(EmailSendRequest request, RequestMetadata data)
        {
            try
            {
                // Load email template
                string emailBody = await LoadEmailTemplateAsync(request.template, request.lang);

                //Replace placeholders
                if (request.placeholders != null)
                {
                    foreach (var placeholder in request.placeholders)
                    {
                        emailBody = emailBody.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
                    }
                }

                var emailSccess = new List<string>();
                if (request.is_singal_mail)
                {
                    foreach (var email in request.to)  // Send separate email for each recipient
                    {
                        EmailRequest singleMail= new EmailRequest
                        {
                            to = new List<string> { email },
                            cc = request.cc,
                            bcc = request.bcc,
                            subject = request.subject,
                            body = emailBody
                        };

                        // Send email individually
                        var sand = await _iEmailGateway.SendEmailAsync(singleMail);
                        if(!sand.IsError)
                        {
                            emailSccess.Add(email);
                        }
                    }
                }
                else
                {
                    //Prepare email payload
                    EmailRequest emailRequest = new EmailRequest
                    {
                        to = request.to,
                        cc = request.cc,
                        bcc = request.bcc,
                        subject = request.subject,
                        body = emailBody
                    };

                    //Send email
                    var send = await _iEmailGateway.SendEmailAsync(emailRequest);
                    if (!send.IsError)
                    {
                        request.to.ForEach(to =>
                        {
                            emailSccess.Add(to);
                        });
                    }
                }

                return await HandleEmailResponse(request.template, emailSccess, data.lang);
            }
            catch(Exception ex)
            {
                return new Response<EmailSccessModel> {Succeeded = false, Message = "SendMailsFailed", Errors = new List<string> { ex.Message }, Data = null };
            }
        }
        private async Task<string> LoadEmailTemplateAsync(string templateName, string lang)
        {
            string templatePath = Path.Combine("Templates", lang, $"{templateName}.html");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Template {templateName} not found.");

            return await File.ReadAllTextAsync(templatePath, Encoding.UTF8);
        }

        private async Task<Response<EmailSccessModel>> HandleEmailResponse(string template, List<string> emailSuccess, string lang)
        {
            var emailSuccessModel = new EmailSccessModel { to = emailSuccess };
            bool isSuccess = emailSuccess.Count > 0;
            string messageKey;

            switch (template)
            {
                case "AdminRequestedReplacementCar":
                case "RequestedReplacementCar":
                    messageKey = "RequestCarSuccess";
                    break;
                default:
                    messageKey = isSuccess ? "SendMailsSuccess" : "SendMailsFailed";
                    break;
            }

            return new Response<EmailSccessModel>(
                emailSuccessModel,
                messageKey,
                isSuccess
            );
        }
    }
}
