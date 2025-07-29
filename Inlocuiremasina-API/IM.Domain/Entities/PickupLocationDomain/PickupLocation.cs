using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.PickupLocationDomain
{
    public class PickupLocation
    {
        [Key]
        public int pl_id { get; set; }
        public string pl_lang { get; set; }
        public string pl_name { get; set; }
        public string pl_lat { get; set; }
        public string pl_long { get; set; }
        public string pl_address { get; set; }
        public string pl_landmark { get; set; }
        public string pl_pincode { get; set; }
        public string pl_map_iframe { get; set; }
        public bool pl_is_active { get; set; }
        public int pl_created_by { get; set; }
        public DateTime pl_created_at { get; set; }
        public int pl_updated_by { get; set; }
        public DateTime pl_updated_at { get; set; }
    }
}
