namespace Auth.Dto.V1.AdminModel.Request
{
    public class CreateAdminRequest
    {
        public string user_full_Name { get; set; }
        public int user_fk_role_id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
