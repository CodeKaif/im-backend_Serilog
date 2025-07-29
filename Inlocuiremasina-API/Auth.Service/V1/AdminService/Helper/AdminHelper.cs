using Auth.Domain.Entities.MasterRoleDomain;
using Auth.Dto.V1.AdminModel.Request;
using Middleware.V1.Request.Model;

namespace Auth.Service.V1.AdminService.Helper
{
    public class AdminHelper
    {
        public MasterRole MapMasterRole(MasterRoleAddModel request, RequestMetadata data)
        {
            return new MasterRole
            {
                role_name = request.role_name,
                role_is_editable = request.role_is_editable,
                role_is_super = request.role_is_super,
                role_permission = request.role_permission,
                role_created_by = data.userId,
                role_created_at = DateTime.UtcNow,
                role_updated_by = data.userId,
                role_updated_at = DateTime.UtcNow,
            };
        }
    }
}
