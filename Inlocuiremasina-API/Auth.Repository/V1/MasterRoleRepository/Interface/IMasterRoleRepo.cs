using Auth.Domain.Entities.MasterRoleDomain;
using Auth.Dto.V1.AdminModel.DataModel;
using Auth.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace Auth.Repository.V1.MasterRoleRepositorey.Interface
{
    public interface IMasterRoleRepo : IGenericRepositoryAsync<MasterRole>
    {
        Task<(IReadOnlyList<MasterRoleDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<MasterRoleDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
