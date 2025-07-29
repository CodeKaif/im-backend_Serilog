using System.ComponentModel.DataAnnotations;

namespace IM.Dto.V1.Car.Request
{
     public class CarImportModel
    {
        public string cr_lang { get; set; }
        public string cr_name { get; set; }
        public string cr_model { get; set; }
        public string cr_image { get; set; }
        public string cr_images { get; set; }
        public string cr_fk_cc_name { get; set; }
        public bool cr_is_available { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string cr_desc { get; set; }
    }
}
