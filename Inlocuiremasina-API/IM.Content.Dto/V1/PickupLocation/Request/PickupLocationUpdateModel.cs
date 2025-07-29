namespace IM.Dto.V1.PickupLocation.Request
{
    public class PickupLocationUpdateModel
    {
        public int pl_id { get; set; }
        public string? pl_lang { get; set; }
        public string? pl_name { get; set; }
        public string? pl_lat { get; set; }
        public string? pl_long { get; set; }
        public string? pl_address { get; set; }
        public string? pl_landmark { get; set; }
        public string? pl_pincode { get; set; }
        public string? pl_map_iframe { get; set; }
    }
}
