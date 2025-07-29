using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.EmailRecipientDomain
{
    public class EmailRecipient
    {
        [Key]
        public int er_id { get; set; }
        public string er_email { get; set; }
        public bool er_is_active { get; set; }
        public DateTime er_created_at { get; set; }
        public int er_created_by { get; set; }
        public int er_updated_by { get; set; }
        public DateTime er_updated_at { get; set; }
    }
}
