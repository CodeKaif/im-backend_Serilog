using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Notification.Service.V1.ApplicationLogsService.Interface
{
    public interface IApplicationLogsSrvc
    {
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
    }
}
