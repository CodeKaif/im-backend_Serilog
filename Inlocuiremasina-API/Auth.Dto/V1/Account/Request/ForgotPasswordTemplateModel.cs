namespace Auth.Dto.V1.Account.Request
{
    public class ForgotPasswordTemplateModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }

    }
}
