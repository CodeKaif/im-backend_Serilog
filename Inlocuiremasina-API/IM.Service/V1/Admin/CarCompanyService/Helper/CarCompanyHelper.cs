using IM.Domain.Entities.CarCompanyDomain;
using IM.Dto.V1.CarCompany.Request;
using Middleware.V1.Request.Model;

namespace IM.Service.V1.Admin.CarCompanyService.Helper
{
    public class CarCompanyHelper
    {
        public CarCompany MapCarCompany(CarCompanyAddModel request, RequestMetadata data)
        {
            return new CarCompany
            {
                cc_name = request.cc_name,
                cc_image = request.cc_image,
                cc_desc = request.cc_desc,
                cc_lang = data.lang,
                cc_is_active = true,
                cc_created_by = data.userId,
                cc_created_at = DateTime.UtcNow,
                cc_updated_by = data.userId,
                cc_updated_at = DateTime.UtcNow,
            };
        }
    }
}
