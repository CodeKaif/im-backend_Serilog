using Auth.Domain.Entities.ApplicationUserDomain;
using Auth.Dto.V1.AdminModel.DataModel;
using Auth.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace Auth.Repository.V1.UserRepository.Interface
{
    public interface IUserRepo : IGenericRepositoryAsync<ApplicationUser>
    {
        Task<(IReadOnlyList<ApplicationUserDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<ApplicationUserDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
