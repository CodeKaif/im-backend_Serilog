using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Middleware.V1.Request.Model;
using System.IdentityModel.Tokens.Jwt;

namespace Middleware.V1.Request
{
    public class ExtractUserIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public ExtractUserIdMiddleware(RequestDelegate next,
                                        IOptionsMonitor<JwtBearerOptions> jwtOptions)
        {
            _next = next;
            _tokenValidationParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
        }

        //public async Task Invoke(HttpContext context, RequestMetadata requestMetadata)
        //{
        //    var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        //    if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        //    {
        //        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        //        var handler = new JwtSecurityTokenHandler();

        //        if (handler.CanReadToken(token))
        //        {
        //            var jwtToken = handler.ReadJwtToken(token);
        //            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id"); // Ensure claim name matches your token

        //            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        //            {
        //                requestMetadata.userId = userId;
        //            }
        //        }
        //    }

        //    // Call the next middleware in the pipeline
        //    await _next(context);
        //}

        public async Task Invoke(HttpContext context, RequestMetadata requestMetadata)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            var language = requestMetadata.lang ?? "en"; //context.Request.Headers["Accept-Language"].FirstOrDefault() ?? "en";

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();

                try
                {
                    // Validate token (will check for expiration, signature, issuer, etc.)
                    var principal = handler.ValidateToken(token, _tokenValidationParameters, out _);

                    var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "user_id");

                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                    {
                        requestMetadata.userId = userId;
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    // Handle expired token
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(GetLocalizedMessage(language, "TokenExpired"));
                    return;
                }
                catch (Exception)
                {
                    // Handle invalid token
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(GetLocalizedMessage(language, "InvalidToken"));
                    return;
                }
            }

            // Proceed to the next middleware if everything is fine
            await _next(context);
        }

        private string GetLocalizedMessage(string language, string key)
        {
            return language switch
            {
                // Romanian
                "ro" when key == "TokenExpired" => "Tokenul a expirat.",
                "ro" when key == "InvalidToken" => "Token invalid.",

                // English 
                "en" when key == "TokenExpired" => "Token has expired.",
                "en" when key == "InvalidToken" => "Invalid token.",

                // Hindi
                "hi" when key == "TokenExpired" => "टोकन की समय सीमा समाप्त हो गई है।",
                "hi" when key == "InvalidToken" => "अमान्य टोकन।",

                // Default (English)
                _ when key == "TokenExpired" => "Token has expired.",
                _ when key == "InvalidToken" => "Invalid token.",
                _ => "An error occurred."
            };
        }

    }
}
