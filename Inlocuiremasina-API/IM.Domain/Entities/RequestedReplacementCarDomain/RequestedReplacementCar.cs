using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.RequestedReplacementCarDomain
{
    public class RequestedReplacementCar
    {
        [Key]
        public int rrc_id { get; set; }
        public string rrc_lang { get; set; }
        public string rrc_fullname { get; set; }
        public string rrc_cnt_code { get; set; }
        [EmailAddress]
        public string rrc_email { get; set; }
        public string rrc_mobile { get; set; }
        public string rrc_pickup_location { get; set; }
        public string rrc_license_plate { get; set; }
        public int rrc_fk_cr_id { get; set; }
        public string rrc_fk_cr_name { get; set; }
        public string rrc_fk_cr_model { get; set; }
        public int rrc_fk_ic_id { get; set; }
        public string rrc_fk_ic_name { get; set; }
        public string rrc_other_info { get; set; }
        public bool rrc_is_active { get; set; }
        public bool rrc_admin_mail_sent { get; set; }
        public bool rrc_user_mail_sent { get; set; }
        public DateTime rrc_created_at { get; set; }
        public int rrc_created_by { get; set; }
        public int rrc_updated_by { get; set; }
        public DateTime rrc_updated_at { get; set; }
    }
}
