namespace IM.Dto.V1.CarCompany.DataModel
{
    public class CarCompanyDto
    {
        public int cc_id { get; set; }
        public string cc_lang { get; set; }
        public string cc_name { get; set; }
        public string cc_image { get; set; }
        public string cc_desc { get; set; }
        public bool cc_is_active { get; set; } = true;
        public DateTime cc_created_at { get; set; }
        public int cc_created_by { get; set; }
        public int cc_updated_by { get; set; }
        public DateTime cc_updated_at { get; set; }
    }
}
