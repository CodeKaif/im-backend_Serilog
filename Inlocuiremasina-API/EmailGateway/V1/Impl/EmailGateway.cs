using EmailGateway.V1.Interface;
using EmailGateway.V1.Model;
using Microsoft.Extensions.Options;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;
using System.Net.Mail;
using System.Text;

namespace EmailGateway.V1.Impl
{
    public class EmailGateway : IEmailGateway
    {
        private readonly EmailConfiguration _emailSettings;
        private readonly ResponseHelper _responseHelper;
        public EmailGateway( IOptions<EmailConfiguration> emailSettings, ResponseHelper responseHelper)
        {
            _emailSettings = emailSettings?.Value;
            _responseHelper = responseHelper;
        }

        public async Task<CommonResponse> SendEmailAsync(EmailRequest request, EmailConfiguration emailSetting = null)
        {
            try
            {
                // if external email settings are null use default
                if (emailSetting == null)
                {
                    emailSetting = _emailSettings;
                }

                var send = await SendAsync(request, emailSetting);
                if (send.IsError)
                {
                    return _responseHelper.CreateResponse("SendMailsFailed", send.Message, 500, null, true);
                }
                return _responseHelper.CreateResponse("SendMailsSuccess", true, 200, null, false);
            }
            catch (Exception ex)
            {
                return _responseHelper.CreateResponse("SendMailsFailed", ex.Message, 500, null, true);
            }
        }

        private async Task<CommonResponse> SendAsync(EmailRequest request, EmailConfiguration emailSetting)
        {
            using (var client = new SmtpClient(emailSetting.Server, emailSetting.Port))
            {
                try
                {
                    client.Credentials = new System.Net.NetworkCredential(emailSetting.Username, emailSetting.Password);
                    client.EnableSsl = true;

                    // mail message setup
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(emailSetting.FromEmail, emailSetting.FromName);
                    request.to.ForEach (to =>
                    {
                        mailMessage.To.Add(to);
                    }) ;
                    request.bcc?.ForEach(bcc =>
                    {
                        mailMessage.Bcc.Add(bcc);
                    });
                    request.cc?.ForEach(cc =>
                    {
                        mailMessage.Bcc.Add(cc);
                    });
                    mailMessage.Subject = request.subject;
                    mailMessage.Body = request.body;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.SubjectEncoding = Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;

                    // send email
                    await client.SendMailAsync(mailMessage);
                    client.Dispose();
                    return _responseHelper.CreateResponse("SendMailsSuccess", true, 200, null, false);
                }
                catch (Exception exp)
                {
                    return _responseHelper.CreateResponse("SendMailsFailed", exp.Message, 500, null, true);
                }
            }
        }
    }
}
