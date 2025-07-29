using ConfigurationModel.FileUploaderSetting;
using FileUploader.V1.FileUploadSrvc.Model;
using Microsoft.AspNetCore.Http;

namespace FileUploader.V1.FileUploadSrvc.Interface
{
    public interface IFileUploadService
    {
        Task<FileUploadResponse> UploadFileAsync(IFormFile file, string directory = null, FileUploaderConfigurationSetting fileUploaderSetting = null);        
    }
}
