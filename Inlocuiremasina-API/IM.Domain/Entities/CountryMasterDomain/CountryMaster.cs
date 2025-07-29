using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.CountryMasterDomain
{
    public class CountryMaster
    {
        [Key]
        public int cnt_id { get; set; }

        public string cnt_lang { get; set; }

        public string cnt_code { get; set; }

        public string cnt_name { get; set; }

        public string cnt_flag { get; set; }

        public string cnt_timezone { get; set; }

        public string cnt_config { get; set; }

        public bool cnt_is_active { get; set; }

        public DateTime cnt_created_at { get; set; }

        public int cnt_created_by { get; set; }

        public int cnt_updated_by { get; set; }

        public DateTime cnt_updated_at { get; set; }
    }
}
