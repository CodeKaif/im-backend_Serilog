using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.RequestCarDomain
{
    public class RequestCar
    {
        [Key]
        public int rc_id { get; set; }
        public string rc_lang { get; set; }
        public string rc_layout { get; set; }
        public string rc_form { get; set; }
        public string rc_success { get; set; }
        public string rc_failure { get; set; }
        public bool rc_is_active { get; set; }
        public int rc_created_by { get; set; }
        public DateTime rc_created_at { get; set; }
        public int rc_updated_by { get; set; }
        public DateTime rc_updated_at { get; set; }
    }
}
