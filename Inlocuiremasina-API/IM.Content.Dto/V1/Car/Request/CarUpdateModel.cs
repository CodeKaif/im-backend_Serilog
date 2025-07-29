namespace IM.Dto.V1.Car.Request
{
    public class CarUpdateModel
    {
        public int cr_id { get; set; }
        public string cr_lang { get; set; }
        public string cr_name { get; set; }
        public string cr_model { get; set; }
        public string cr_image { get; set; }
        public string cr_images { get; set; }
        public int cr_fk_cc_id { get; set; }
        public bool cr_is_available { get; set; }
        public string cr_desc { get; set; }
    }
}
