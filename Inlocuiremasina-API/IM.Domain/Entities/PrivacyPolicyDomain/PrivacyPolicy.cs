using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.PrivacyPolicyDomain
{
    public class PrivacyPolicy
    {
        [Key]
        public int pp_id { get; set; }
        public string pp_lang { get; set; }
        public string pp_content { get; set; }
        public string pp_seo { get; set; }
        public bool pp_is_active { get; set; }
        public int pp_created_by { get; set; }
        public DateTime pp_created_at { get; set; }
        public int pp_updated_by { get; set; }
        public DateTime pp_updated_at { get; set; }
    }
}
