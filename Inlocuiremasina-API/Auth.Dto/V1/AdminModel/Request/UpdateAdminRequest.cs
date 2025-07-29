namespace Auth.Dto.V1.AdminModel.Request
{
    public class UpdateAdminRequest
    {
        public string user_id { get; set; }
        public string user_full_Name { get; set; }
        public int user_fk_role_id { get; set; }
    }
}
