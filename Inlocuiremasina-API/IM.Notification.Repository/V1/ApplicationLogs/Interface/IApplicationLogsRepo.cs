using IM.Notification.Domain.Entities.ApplicationLogsDomain;
using IM.Notification.Dto.ApplicationLogs.DataModel;
using IM.Notification.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace IM.Notification.Repository.V1.CarCompanyRepository.Interface
{
    public interface IApplicationLogsRepo : IGenericRepositoryAsync<ApplicationLogs>
    {
        Task<(IReadOnlyList<ApplicationLogsDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<ApplicationLogsDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
