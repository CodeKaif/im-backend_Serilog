using FileUploader.V1.FileUploadSrvc.Impl;
using FileUploader.V1.FileUploadSrvc.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FileUploader.V1.FileUploadSrvc.Extension
{
    public class FileUploadServiceExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFileUploadService, FileUploadService>();
        }
    }
}
