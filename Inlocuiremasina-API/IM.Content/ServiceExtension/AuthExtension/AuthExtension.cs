using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ResponseWrapper.V1.Model;
using System.Net;
using System.Text;

namespace IM.Content.ServiceExtension.AuthExtension
{
    public static class AuthExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(
                            new CommonResponse
                            {
                                Status = HttpStatusCode.Unauthorized,
                                Message = "You are not authorized",
                                Data = { },
                                IsError = true
                            });
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(
                            new CommonResponse
                            {
                                Status = HttpStatusCode.Unauthorized,
                                Message = "You are not authorized to access this resource",
                                Data = { },
                                IsError = true
                            });
                        return context.Response.WriteAsync(result);
                    },
                };
            });
        }
    }
}
