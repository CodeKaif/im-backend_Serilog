namespace Auth.Dto.V1.AdminModel.Request
{
    public class ChangePasswordRequest
    {
        public string user_id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
