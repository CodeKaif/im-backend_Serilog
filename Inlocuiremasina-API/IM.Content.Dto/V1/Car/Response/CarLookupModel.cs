namespace IM.Dto.V1.Car.Request
{
    public class CarLookupRequest
    {
        public string? cr_model { get; set; } = "all";
        public string? cr_fk_cc_name { get; set; } = "all";
        public string? cr_name { get; set; } = "all";
        public bool? cr_is_available { get; set; }
        public bool? cr_is_active { get; set; } = true;
    }
}
