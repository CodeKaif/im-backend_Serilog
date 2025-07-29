using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.LayoutDomain
{
    public class Layout
    {
        [Key]
        public int lyt_id { get; set; }
        public string lyt_lang { get; set; }
        public string lyt_logo { get; set; }
        public string lyt_header { get; set; }
        public string lyt_footer { get; set; }
        public string lyt_config { get; set; }
        public bool lyt_is_active { get; set; }
        public int lyt_created_by { get; set; }
        public DateTime lyt_created_at { get; set; }
        public int lyt_updated_by { get; set; }
        public DateTime lyt_updated_at { get; set; }
    }
}
