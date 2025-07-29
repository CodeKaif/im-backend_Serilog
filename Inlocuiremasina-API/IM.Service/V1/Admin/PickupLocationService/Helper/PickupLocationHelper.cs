using IM.Domain.Entities.PickupLocationDomain;
using IM.Dto.V1.PickupLocation.Request;
using Middleware.V1.Request.Model;

namespace IM.Service.V1.Admin.PickupLocationService.Helper
{
    public class PickupLocationHelper
    {
        public PickupLocation MapPickupLocation(PickupLocationAddModel request, RequestMetadata data)
        {
            return new PickupLocation
            {
                pl_name = request.pl_name,
                pl_lang = data.lang,
                pl_address = request.pl_address,
                pl_is_active = true,
                pl_lat = request.pl_lat,
                pl_long = request.pl_long,
                pl_landmark = request.pl_landmark,
                pl_pincode = request.pl_pincode,
                pl_map_iframe = request.pl_map_iframe,
                pl_created_by = data.userId,
                pl_created_at = DateTime.UtcNow,
                pl_updated_by = data.userId,
                pl_updated_at = DateTime.UtcNow,
            };
        }
    }
}
