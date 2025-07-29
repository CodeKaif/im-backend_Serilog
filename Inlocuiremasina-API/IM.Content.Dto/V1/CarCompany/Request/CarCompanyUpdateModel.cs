namespace IM.Dto.V1.CarCompany.Request
{
    public class CarCompanyUpdateModel
    {
        public int cc_id { get; set; }
        public string? cc_lang { get; set; }
        public string? cc_name { get; set; }
        public string? cc_image { get; set; }
        public string? cc_desc { get; set; }
    }
}
