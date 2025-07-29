using Auth.Dto.V1.AdminModel.Request;
using Auth.Service.V1.AdminService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;

namespace IM.Auth.Controllers.V1.Admin
{
    [Authorize]
    public class AdminController : BaseApiController
    {
        private readonly RequestMetadata _metadata;
        private readonly ResponseHelper _responseHelper;
        private readonly IAdminSrvc _adminService;
        public AdminController(RequestMetadata metadata, IAdminSrvc adminService,
                                    ResponseHelper responseHelper)
        {
            _metadata = metadata;
            _responseHelper = responseHelper;
            _adminService = adminService;
        }

        #region Admin User APIs
        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetByIdAsync(string user_id)
        {
            var response = await _adminService.GetByIdAsync(user_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PagingAsync([FromBody] FilterRequest filter)
        {
           var response = await _adminService.PagingAsync(filter, _metadata);
           return _responseHelper.ResponseWrapper(response);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportAsync([FromBody] FilterRequest filter)
        {
           var response = await _adminService.ExportAsync(filter, _metadata);
           return _responseHelper.ResponseWrapper(response);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] CreateAdminRequest request)
        {
           var response = await _adminService.InsertAsync(request, _metadata);
           return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] UpdateAdminRequest filter)
        {
            var response = await _adminService.UpdateAdminAsync(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest filter)
        {
           
            var response = await _adminService.ChangePassword(filter, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [HttpDelete("{user_id}")]
        public async Task<IActionResult> DeleteAdminByIdAsync(string user_id)
        {
            var response = await _adminService.DeleteAdminByIdAsync(user_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }
        #endregion

        #region Master Role APIs
        [Route("masterrole/{rl_id}")]
        [HttpGet]
        public async Task<IActionResult> GetMasterRoleByIdAsync(int rl_id)
        {
            var response = await _adminService.GetMasterRoleByIdAsync(rl_id, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [Route("masterrole/add")]
        [HttpPost]
        public async Task<IActionResult> AddMasterRoleAsync([FromBody] MasterRoleAddModel request)
        {
            var response = await _adminService.AddMasterRoleAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [Route("masterrole/update")]
        [HttpPut]
        public async Task<IActionResult> UpdateMasterRoleAsync([FromBody] MasterRoleUpdateModel request)
        {
           var response = await _adminService.UpdateMasterRoleAsync(request, _metadata);
           return _responseHelper.ResponseWrapper(response);
        }

        [Route("masterrole")]
        [HttpGet]
        public async Task<IActionResult> LookupMasterRoleAsync()
        {
           var response = await _adminService.LookupMasterRoleAsync( _metadata);
           return _responseHelper.ResponseWrapper(response);
        }

        [Route("masterrole/paging")]
        [HttpPost]
        public async Task<IActionResult> PagingMasterRoleAsync(FilterRequest request)
        {
            var response = await _adminService.PagingMasterRoleAsync( request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [Route("masterrole/export")]
        [HttpPost]
        public async Task<IActionResult> ExportMasterRoleAsync(FilterRequest request)
        {
            var response = await _adminService.ExportMasterRoleAsync(request, _metadata);
            return _responseHelper.ResponseWrapper(response);
        }

        [Route("masterrole/{rl_id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMasterRoleByIdAsync(int rl_id)
        {
           var response = await _adminService.DeleteMasterRoleByIdAsync(rl_id, _metadata);
           return _responseHelper.ResponseWrapper(response);
        }
        #endregion
    }
}
