using System.ComponentModel.DataAnnotations;

namespace Auth.Dto.V1.Account.Request
{
    public class ResetPassword : VerifyResetPasswordTokenRequest
    {
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
