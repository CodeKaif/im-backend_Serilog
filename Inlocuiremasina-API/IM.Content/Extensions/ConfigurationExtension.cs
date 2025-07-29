using ConfigurationModel.CacheSetting;
using ConfigurationModel.FileUploaderSetting;
using ConfigurationModel.JWTSetting;
using ConfigurationModel.ServicesEndpoint;
using Microsoft.Extensions.Options;

namespace IM.Content.Extensions
{
    public class ConfigurationExtension
    {
        public static void RegisterConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            RegisterJWTConfiguration(services, configuration);
            RegisterFileUploadConfiguration(services, configuration);
            RegisterServiceEndpoint(services, configuration);
            RegisterCacheConfiguration(services, configuration);
        }

        // JWT Configuration
        public static void RegisterJWTConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTConfigurationSetting>(configuration.GetSection("JWTSettings"));
        }

        //FileUploader Configuration 
        public static void RegisterFileUploadConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileUploaderConfigurationSetting>(configuration.GetSection("FileUploadSettings"));
        }

        //Internal Client Request Configuration 
        public static void RegisterServiceEndpoint(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServicesEndpoint>(configuration.GetSection("ServicesEndpoint"));
        }

        public static void RegisterCacheConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfigurationSetting>(configuration.GetSection("RedisSettings"));
            services.AddSingleton(sp =>  sp.GetRequiredService<IOptions<CacheConfigurationSetting>>().Value);
        }
    }
}
