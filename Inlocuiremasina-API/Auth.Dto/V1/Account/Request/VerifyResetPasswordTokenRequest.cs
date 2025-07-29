namespace Auth.Dto.V1.Account.Request
{
    public class VerifyResetPasswordTokenRequest : TokenBaseRequest
    {
        public string UserId { get; set; }
    }
}
