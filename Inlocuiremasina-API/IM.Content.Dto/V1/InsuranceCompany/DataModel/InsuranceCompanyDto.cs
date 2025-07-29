namespace IM.Dto.V1.InsuranceCompany.DataModel
{
    public class InsuranceCompanyDto
    {
        public int ic_id { get; set; }
        public string ic_lang { get; set; }
        public string ic_name { get; set; }
        public string ic_image { get; set; }
        public string ic_desc { get; set; }
        public bool ic_is_active { get; set; }
        public DateTime ic_created_at { get; set; }
        public int ic_created_by { get; set; }
        public int ic_updated_by { get; set; }
        public DateTime? ic_updated_at { get; set; }
    }
}
