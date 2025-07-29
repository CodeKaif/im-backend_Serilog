using ConfigurationModel.FileUploaderSetting;
using ConfigurationModel.JWTSetting;
using ConfigurationModel.ServicesEndpoint;
using EmailGateway.V1.Model;

namespace IM.Notification.ServiceExtension.Extension
{
    public class ConfigurationExtension
    {
        public static void RegisterConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            RegisterSMTPConfiguration(services, configuration);
        }

        // SMTP Configuration
        public static void RegisterSMTPConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfiguration>(configuration.GetSection("SmtpSettings"));
        }
    }
}
