using ConfigurationModel.JWTSetting;
using Helper.Ip;
using Helper.RandomString;
using Helper.TokenGenerator.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Helper.TokenGenerator
{
    public class TokenGeneratorHelper
    {
        public static string GenerateJWTToken(IEnumerable<Claim> claims, JWTConfigurationSetting jwtSettings)
        {
            // add ip address on claim
            string ipAddress = IpHelper.GetIpAddress();
            claims.Append(new Claim("ip", ipAddress));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public static RefreshToken GenerateRefreshToken(string ipAddress, int digits)
        {
            return new RefreshToken
            {
                Token = RandomStringHelper.RandomTokenString(digits),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
    }
}
