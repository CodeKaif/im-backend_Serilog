using System.ComponentModel.DataAnnotations;

namespace Auth.Dto.V1.Account.Request
{
    public class TokenBaseRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
