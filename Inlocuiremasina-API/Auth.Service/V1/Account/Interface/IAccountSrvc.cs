using Auth.Dto.V1.Account.Request;
using Microsoft.AspNetCore.Identity.Data;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace Auth.Service.V1.Account.Interface
{
    public interface IAccountSrvc
    {
        Task<CommonResponse> AuthenticateAsync(AuthenticationRequest request, RequestMetadata data, string ipAddress);
        Task<CommonResponse> ForgotPassword(ForgotPasswordRequest request, RequestMetadata data);
        Task<CommonResponse> VerifyResetPasswordToken(VerifyResetPasswordTokenRequest request, RequestMetadata data);
        Task<CommonResponse> ResetPassword(ResetPassword request, RequestMetadata data);
        Task<CommonResponse> Logout(RequestMetadata data);
    }
}
