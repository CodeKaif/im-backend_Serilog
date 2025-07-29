using Auth.Dto.V1.AdminModel.Request;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace Auth.Service.V1.AdminService.Interface
{
    public interface IAdminSrvc
    {
        Task<CommonResponse> InsertAsync(CreateAdminRequest request, RequestMetadata data);
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> GetByIdAsync(string user_id, RequestMetadata data);
        Task<CommonResponse> UpdateAdminAsync(UpdateAdminRequest request, RequestMetadata data);
        Task<CommonResponse> ChangePassword(ChangePasswordRequest request, RequestMetadata data);
        Task<CommonResponse> DeleteAdminByIdAsync(string user_id, RequestMetadata data);
        Task<CommonResponse> DeleteMasterRoleByIdAsync(int rl_id, RequestMetadata data);
        Task<CommonResponse> GetMasterRoleByIdAsync(int rl_id, RequestMetadata data);
        Task<CommonResponse> UpdateMasterRoleAsync(MasterRoleUpdateModel request, RequestMetadata data);
        Task<CommonResponse> AddMasterRoleAsync(MasterRoleAddModel request, RequestMetadata data);
        Task<CommonResponse> PagingMasterRoleAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportMasterRoleAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> LookupMasterRoleAsync(RequestMetadata data);
    }
}
