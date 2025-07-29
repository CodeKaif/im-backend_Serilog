using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Entities.ApplicationUserDomain
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string user_full_Name { get; set; }
        public bool user_is_active { get; set; }
        public int user_fk_role_id { get; set; }
        public string user_fk_role_name { get; set; }
        public bool user_is_owner { get; set; } = false;
        public int user_created_by { get; set; }
        public DateTime user_created_at { get; set; }
        public int user_updated_by { get; set; }
        public DateTime user_updated_at { get; set; }
    }
}
