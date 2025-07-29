using Auth.Domain.Entities.ApplicationUserDomain;
using Auth.Dto.V1.Account.Request;
using Auth.Dto.V1.Account.Response;
using Auth.Repository.V1.MasterRoleRepositorey.Interface;
using Auth.Service.Constant;
using Auth.Service.V1.Account.Interface;
using Azure;
using CommonApiCallHelper.V1.Constant;
using CommonApiCallHelper.V1.Interface;
using ConfigurationModel.FrontendSetting;
using ConfigurationModel.JWTSetting;
using EmailGateway.V1.Model;
using Helper.PasswordHasher;
using Helper.TokenGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Auth.Service.V1.Account.Impl
{
    public class AccountSrvc : IAccountSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTConfigurationSetting _jwtSettings;
        private readonly IMasterRoleRepo _masterRoleRepo;
        private readonly FrontendConfigurationSetting _frontendSettings;
        private readonly ICommonApiCall _commonApiCall;
        public AccountSrvc(UserManager<ApplicationUser> userManager, ResponseHelper responseHelper,
                         SignInManager<ApplicationUser> signInManager,
                         IOptions<JWTConfigurationSetting> jwtSettings,
                         IMasterRoleRepo masterRoleRepo,
                         ICommonApiCall commonApiCall,
                         IOptions<FrontendConfigurationSetting> frontendSettings)
        {
            _userManager = userManager;
            _responseHelper = responseHelper;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _masterRoleRepo = masterRoleRepo;
            _commonApiCall = commonApiCall;
            _frontendSettings = frontendSettings.Value;
        }

        public async Task<CommonResponse> AuthenticateAsync(AuthenticationRequest request, RequestMetadata data, string ipAddress)
        {
            // check user exists or not
            data.lang = "en";
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null || !user.user_is_active)
            {
                return _responseHelper.CreateResponse("AdminNotExist", false, 404, data.lang, false);
            }

            // compare password with requested password
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return _responseHelper.CreateResponse("InValidEmailPassword", false, 404, data.lang, false);
            }

            var masterRole = await _masterRoleRepo.GetFirstOrDefault(x => x.role_id == user.user_fk_role_id);
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RoleNotFound", false, 404, data.lang, false);
            }
            AuthenticationResponse response = new AuthenticationResponse();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.user_full_Name),
                new Claim("user_id", user.Id.ToString())
                //new Claim("roleId", response.UserClient?.MasterRoleId?.ToString())
            };

            AuthenticationResponseUser applicationUser = new AuthenticationResponseUser 
            {
                user_id = user.Id,
                user_full_Name = user.user_full_Name,
                user_email = user.Email,
                user_role = masterRole.role_name,
                permission = JsonSerializer.Serialize(masterRole.role_permission)
            };

            response.JWTUserToken = TokenGeneratorHelper.GenerateJWTToken(claims, _jwtSettings);
            response.RefreshUserToken = TokenGeneratorHelper.GenerateRefreshToken(ipAddress, 40).Token;
            response.user = applicationUser;

            return _responseHelper.CreateResponse("LoginSuccess", response, 200, data.lang, false);
        }

        #region Forgot Password
        public async Task<CommonResponse> ForgotPassword(ForgotPasswordRequest request, RequestMetadata data)
        {
            data.lang = "en";
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !user.user_is_active)
            {
               return _responseHelper.CreateResponse("EmailNotExist", false, 404, data.lang, true);
            }


            // generate reset password token 
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var templateModel = new ForgotPasswordTemplateModel()
            {
                Code = encodedToken,
                Name = user.UserName,
                UserId = user.Id.ToString()
            };

            // send email
            await PasswordResetRequest(new List<string> { request.Email }, templateModel, data);

            return _responseHelper.CreateResponse("EmailSendSuccess", true, 200, data.lang, false);
        }

        public async Task<CommonResponse> PasswordResetRequest(List<string> email, ForgotPasswordTemplateModel request, RequestMetadata data)
        {
           
            string resetPasswordUrl = string.Format("{0}{1}", _frontendSettings.FrontendUrl,string.Format(_frontendSettings.AccountUrls.ResetPasswordUrl, request.UserId, request.Code));

            EmailSendRequest emailSend = new EmailSendRequest
            {
                to = email,
                template = "PasswordResetRequest",
                subject = "Password Reset Request",
                is_singal_mail = true,
                lang = "en",
                placeholders = new Dictionary<string, string>
                {
                    { "user_name", request.Name},
                    { "reset_link",resetPasswordUrl},
                    
                }
            };

           var responce = await _commonApiCall.PostDataAsync<ResponseWrapper.V1.Model.Response<EmailSccessModel>>(emailSend, ServiceEndpointConstants.NotificationApi, ApiEndPointConstant.SendEmailAsync());
           return _responseHelper.CreateResponse("EmailSendSuccess", responce, 200, "en", false);
        }
        #endregion

        #region Reset Password 

        public async Task<CommonResponse>  VerifyResetPasswordToken(VerifyResetPasswordTokenRequest request, RequestMetadata data)
        {
            data.lang = "en";
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null || !user.user_is_active)
            {
                return _responseHelper.CreateResponse("InvalidLink", false, 400, data.lang, false);

            }
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.VerifyUserTokenAsync(user, this._userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedToken);
            if (!result)
            {
                return _responseHelper.CreateResponse("InvalidLink", false, 400, data.lang, false);
            }

            return _responseHelper.CreateResponse("Sccess", true, 200, data.lang, false);
        }

        public async Task<CommonResponse> ResetPassword(ResetPassword request, RequestMetadata data)
        {
            data.lang = "en";
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null || !user.user_is_active)
            {
                return _responseHelper.CreateResponse("InvalidLink", false, 400, data.lang, false);
            }
            var hash = PasswordHasherHelper.Encrypt(request.Password);
            user.PasswordHash = hash;
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.Password);
            if (!result.Succeeded)
            {
                return _responseHelper.CreateResponse("InvalidLink", false, 400, data.lang, false);
            }
            await _userManager.UpdateAsync(user);

            return _responseHelper.CreateResponse("PasswordResetSuccess", true, 200, data.lang, false);
        }
        #endregion

        #region Logout
        public async Task<CommonResponse> Logout(RequestMetadata data)
        {
            data.lang = "en";
            await _signInManager.SignOutAsync();
            return _responseHelper.CreateResponse("LogoutSucess", true, 200, data.lang, false);
        }
        #endregion

    }
}
