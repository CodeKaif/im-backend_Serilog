using IM.Domain.Entities.InsuranceCompanyDomain;
using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.RequestedReplacementCarModel.Request;
using Middleware.V1.Request.Model;

namespace IM.Service.V1.Guest.RequestedReplacementCarService.Helper
{
    public class RequestedReplacementCarGuestHelper
    {
        public RequestedReplacementCar MapRequestedReplacementCar(AddRequestedReplacementCarRequest request, InsuranceCompany insuranceCompany, RequestMetadata data)
        {
            return new RequestedReplacementCar
            {
                rrc_lang = data.lang,
                rrc_fullname = request.rrc_fullname,
                rrc_cnt_code = request.rrc_cnt_code,
                rrc_email = request.rrc_email,
                rrc_mobile = request.rrc_mobile,
                rrc_pickup_location = request.rrc_pickup_location,
                rrc_license_plate = request.rrc_license_plate,
                rrc_fk_cr_model = request.rrc_fk_cr_model,
                rrc_fk_ic_id = insuranceCompany.ic_id,
                rrc_fk_ic_name = insuranceCompany.ic_name,
                rrc_other_info = request.rrc_other_info,
                rrc_admin_mail_sent = true,
                rrc_user_mail_sent = true,
                rrc_is_active = true,
                rrc_created_by = data.userId,
                rrc_created_at = DateTime.UtcNow,
                rrc_updated_by = data.userId,
                rrc_updated_at = DateTime.UtcNow,
            };
        }
    }
}
