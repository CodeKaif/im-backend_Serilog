using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Entities.HomePageDomain
{
    public class HomePage
    {
        [Key]
        public int hp_id { get; set; }
        public string hp_lang { get; set; }
        public string hp_banner { get; set; }
        public string hp_take_care { get; set; }
        public string hp_car_step { get; set; }
        public string hp_car { get; set; }
        public string hp_how_it_work { get; set; }
        public string hp_find_us { get; set; }
        public string hp_seo { get; set; }
        public bool hp_is_active { get; set; }
        public int hp_created_by { get; set; }
        public DateTime hp_created_at { get; set; }
        public int hp_updated_by { get; set; }
        public DateTime hp_updated_at { get; set; }
    }
}
