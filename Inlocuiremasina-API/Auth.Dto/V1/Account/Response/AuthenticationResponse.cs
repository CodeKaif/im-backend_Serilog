using System.Text.Json.Serialization;

namespace Auth.Dto.V1.Account.Response
{
    public class AuthenticationResponse
    {
        public string JWTUserToken { get; set; }
        [JsonIgnore]
        public string RefreshUserToken { get; set; }
        public AuthenticationResponseUser user { get; set; }
    }

    public class AuthenticationResponseUser
    {
        public int user_id { get; set; }
        public string user_full_Name { get; set; }
        public string user_email { get; set; }
        public string user_role { get; set; }
        public string permission { get; set; }
    }
}
