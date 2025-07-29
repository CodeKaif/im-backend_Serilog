using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ConfigurationModel.FileUploaderSetting;
using FileUploader.V1.FileUploadSrvc.Interface;
using FileUploader.V1.FileUploadSrvc.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FileUploader.V1.FileUploadSrvc.Impl
{
    public class FileUploadService : IFileUploadService
    {
        private readonly FileUploaderConfigurationSetting _fileUploaderSettings;

        public FileUploadService(IOptions<FileUploaderConfigurationSetting> fileUploaderSettings)
        {
            _fileUploaderSettings = fileUploaderSettings?.Value;
        }

        public async Task<FileUploadResponse> UploadFileAsync(IFormFile file, string directory = null, FileUploaderConfigurationSetting fileUploaderSetting = null)
        {
            // if external email settings are null then use default
            if (fileUploaderSetting == null)
            {
                fileUploaderSetting = _fileUploaderSettings;
            }

            return await FileUploadInAzureBlobAsync(file, fileUploaderSetting, directory);
         
        }

        private async Task<FileUploadResponse> FileUploadInAzureBlobAsync(IFormFile file, FileUploaderConfigurationSetting fileUploaderSetting, string directory = null)
        {
            try
            {
                BlobServiceClient _blobServiceClient = new BlobServiceClient(GetConnectionString(fileUploaderSetting));

                var containerClient = _blobServiceClient.GetBlobContainerClient(fileUploaderSetting.ContainerName);

                // Will create the container if it doesn't exist
                await containerClient.CreateIfNotExistsAsync();

                string fileName = Regex.Replace(Guid.NewGuid() + "-" + Regex.Replace(file.FileName, @"[^0-9a-zA-Z.]+", ""), @"\s+", "_");

                string blobPath = string.IsNullOrWhiteSpace(directory)
                                        ? fileName
                                        : $"{directory}/{fileName}";

                var blobClient = containerClient.GetBlobClient(blobPath);

                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = GetContentType(file.FileName) // Set the correct content type
                };

                using var stream = file.OpenReadStream();
                //await blobClient.UploadAsync(stream, overwrite: true);
                await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });

                return new FileUploadResponse { ImageUrl = blobClient.Uri.AbsoluteUri, success = true };
            }
            catch
            {
                return new FileUploadResponse { ImageUrl = "", success = false };
            }
        }

        private string GetConnectionString(FileUploaderConfigurationSetting fileUploaderSetting)
        {         
            return $"DefaultEndpointsProtocol={fileUploaderSetting.DefaultEndpointsProtocol};" +
                   $"AccountName={fileUploaderSetting.AccountName};" +
                   $"AccountKey={fileUploaderSetting.AccountKey};" +
                   $"EndpointSuffix={fileUploaderSetting.EndpointSuffix}";
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLower();

            return extension switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".svg" => "image/svg+xml",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".json" => "application/json",
                _ => "application/octet-stream"
            };
        }
    }
}
