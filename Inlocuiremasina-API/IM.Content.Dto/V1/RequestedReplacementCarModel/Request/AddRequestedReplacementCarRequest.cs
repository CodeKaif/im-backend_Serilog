using System.ComponentModel.DataAnnotations;

namespace IM.Dto.V1.RequestedReplacementCarModel.Request
{
    public class AddRequestedReplacementCarRequest
    {
        public string rrc_lang { get; set; }

        public string rrc_fullname { get; set; }

        public string rrc_cnt_code { get; set; }

        [EmailAddress]
        public string rrc_email { get; set; }

        public string rrc_mobile { get; set; }

        public string rrc_pickup_location { get; set; }

        public string rrc_license_plate { get; set; }
        public string rrc_fk_cr_model { get; set; }

        public int rrc_fk_ic_id { get; set; }

        public string rrc_other_info { get; set; }
    }
}
