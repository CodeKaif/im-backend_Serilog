using FileUploader.V1.FileUploadSrvc.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace FileUploader
{
    public static class FileUploaderRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            FileUploadServiceExtension.RegisterServices(services);
        }
    }
}
