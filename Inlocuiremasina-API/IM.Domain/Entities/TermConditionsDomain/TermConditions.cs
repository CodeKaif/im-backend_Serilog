using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.TermConditionsDomain
{
    public class TermConditions
    {
        [Key]
        public int tc_id { get; set; }

        public string tc_lang { get; set; }

        public string tc_content { get; set; } // JSON content

        public string tc_seo { get; set; } // JSON content

        public bool tc_is_active { get; set; }

        public int tc_created_by { get; set; }

        public DateTime tc_created_at { get; set; }

        public int tc_updated_by { get; set; }

        public DateTime tc_updated_at { get; set; }
    }
}
