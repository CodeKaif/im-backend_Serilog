namespace Auth.Dto.V1.AdminModel.Request
{
    public class MasterRoleUpdateModel
    {
        public int role_id { get; set; }
        public string role_name { get; set; }
        public bool role_is_editable { get; set; } = true;
        public bool role_is_super { get; set; } = false;
        public string role_permission { get; set; }
    }
}
