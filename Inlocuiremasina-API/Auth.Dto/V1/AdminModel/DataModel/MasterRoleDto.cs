namespace Auth.Dto.V1.AdminModel.DataModel
{
    public class MasterRoleDto
    {
        public int role_id { get; set; }
        public string role_name { get; set; }
        public bool role_is_editable { get; set; } = true;
        public bool role_is_super { get; set; } = false;
        public string role_permission { get; set; } // JSON
        public int role_created_by { get; set; }
        public DateTime role_created_at { get; set; }
        public int role_updated_by { get; set; }
        public DateTime role_updated_at { get; set; }
    }
}
