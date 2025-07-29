using ConfigurationModel.FileUploaderSetting;
using ConfigurationModel.FrontendSetting;
using ConfigurationModel.JWTSetting;
using ConfigurationModel.ServicesEndpoint;

namespace IM.Auth.Extensions
{
    public class ConfigurationExtension
    {
        public static void RegisterConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            RegisterJWTConfiguration(services, configuration);
            RegisterFrontendConfiguration(services, configuration);
            RegisterServiceEndpoint(services, configuration);
        }

        // JWT Configuration
        public static void RegisterJWTConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTConfigurationSetting>(configuration.GetSection("JWTSettings"));
        }

        //FileUploader Configuration // If need to use call from in RegisterConfiguration
        public static void RegisterFileUploadConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileUploaderConfigurationSetting>(configuration.GetSection("FileUploadSettings"));
        }
        public static void RegisterFrontendConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FrontendConfigurationSetting>(configuration.GetSection("FrontendSettings"));
        }

        //Internal Client Request Configuration 
        public static void RegisterServiceEndpoint(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServicesEndpoint>(configuration.GetSection("ServicesEndpoint"));
        }
    }
}
