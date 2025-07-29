using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Dto.V1.InsuranceCompany.Request;
using Middleware.V1.Request.Model;

namespace IM.Service.V1.Admin.InsuranceCompanyService.Helper
{
    public class InsuranceCompanyHelper
    {
        public InsuranceCompany MapInsuranceCompany(InsuranceCompanyAddModel request, RequestMetadata data)
        {
            return new InsuranceCompany
            {
                ic_name = request.ic_name,
                ic_image = request.ic_image,
                ic_desc = request.ic_desc,
                ic_lang = data.lang,
                ic_is_active = true,
                ic_created_by = data.userId,
                ic_created_at = DateTime.UtcNow,
                ic_updated_by = data.userId,
                ic_updated_at = DateTime.UtcNow,
            };
        }
    }
}
