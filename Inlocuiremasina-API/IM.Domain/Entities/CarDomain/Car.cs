using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.CarDomain
{
    public class Car
    {
        [Key]
        public int cr_id { get; set; }
        public string cr_lang { get; set; }
        public string cr_name { get; set; }
        public string cr_model { get; set; }
        public string cr_image { get; set; }
        public string cr_images { get; set; }
        public int cr_fk_cc_id { get; set; }
        public string cr_fk_cc_name { get; set; }
        public bool cr_is_available { get; set; }
        public string cr_desc { get; set; }
        public bool cr_is_active { get; set; }
        public DateTime cr_created_at { get; set; }
        public int cr_created_by { get; set; }
        public int cr_updated_by { get; set; }
        public DateTime cr_updated_at { get; set; }
    }
}
