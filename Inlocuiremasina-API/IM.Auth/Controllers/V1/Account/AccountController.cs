using Auth.Dto.V1.Account.Request;
using Auth.Service.V1.Account.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Auth.Controllers.V1.Account
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IAccountSrvc _accountService;
        public AccountController(RequestMetadata metadata, ResponseHelper responseHelper,
                                 IAccountSrvc accountService)
        {
            _metadata = metadata;
            _responseHelper = responseHelper;
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            var response = await _accountService.AuthenticateAsync(request, _metadata, GenerateIPAddress());

            return _responseHelper.ResponseWrapper(response);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var response = await _accountService.ForgotPassword(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetTokenm(VerifyResetPasswordTokenRequest request)
        {
            var response = await _accountService.VerifyResetPasswordToken(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword request)
        {
            var response = await _accountService.ResetPassword(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var response = await _accountService.Logout(_metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
