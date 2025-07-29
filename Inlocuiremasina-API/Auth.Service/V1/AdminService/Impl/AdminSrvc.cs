using Auth.Domain.Entities.ApplicationUserDomain;
using Auth.Dto.V1.AdminModel.Request;
using Auth.Dto.V1.AdminModel.Response;
using Auth.Repository.V1.MasterRoleRepositorey.Interface;
using Auth.Repository.V1.UserRepository.Interface;
using Auth.Service.V1.AdminService.Helper;
using Auth.Service.V1.AdminService.Interface;
using AutoMapper;
using Azure.Core;
using Helper.PasswordHasher;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Identity;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace Auth.Service.V1.AdminService.Impl
{
    public class AdminSrvc : IAdminSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMasterRoleRepo _masterRoleRepo;
        private readonly IUserRepo _userRepo;
        private readonly AdminHelper _adminHelper;
        private readonly IMapper _mapper;

        public AdminSrvc(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ResponseHelper responseHelper, 
            IUserRepo userRepo, 
            IMasterRoleRepo masterRoleRepo,
            IMapper mapper)
        {
            _userManager = userManager;
            _responseHelper = responseHelper;
            _masterRoleRepo = masterRoleRepo;
            _adminHelper = new AdminHelper();
            _mapper = mapper;
            _userRepo = userRepo;
             _signInManager = signInManager;
        }

        #region User Apis
        public async Task<CommonResponse> InsertAsync(CreateAdminRequest request, RequestMetadata data)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.Email);
            if (userWithSameUserName != null)
            {
                return _responseHelper.CreateResponse("EmailAlreadyExist", false, 409, data.lang, false);
            }
            
            var masterRole = await _masterRoleRepo.GetFirstOrDefault(x => x.role_id == request.user_fk_role_id);
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RoleNotFound", false, 404, data.lang, false);
            }
            var user = new ApplicationUser
            {
                user_full_Name = request.user_full_Name,
                user_is_active = true,
                user_created_at = DateTime.UtcNow,
                user_created_by = data.userId,
                user_updated_at = DateTime.UtcNow,
                user_updated_by = data.userId,
                user_fk_role_id = request.user_fk_role_id,
                user_fk_role_name = masterRole.role_name,
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true,
                user_is_owner = false,
            };

            var hash = PasswordHasherHelper.Encrypt(request.Password);
            user.PasswordHash = hash;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var rr = _responseHelper.CreateResponse("AdminAddSuccess", true, 200, data.lang, false);
                return rr;
            }
            else
            {
                throw new Exception(result.Errors.ToString());
            }
        }
        public async Task<CommonResponse> UpdateAsync(UpdateAdminRequest request, RequestMetadata data)
        {
            // Fetch the record using repository
            var user = await _userManager.FindByIdAsync(request.user_id);

            if (user == null)
            {
                return _responseHelper.CreateResponse("EmailAlreadyExist", false, 409, data.lang, false);
            }

            var masterRole = await _masterRoleRepo.GetFirstOrDefault(x => x.role_id == request.user_fk_role_id);
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RoleNotFound", false, 404, data.lang, false);
            }
            user.user_full_Name = request.user_full_Name;
            user.user_fk_role_id = request.user_fk_role_id;
            user.user_fk_role_name = masterRole.role_name;
            user.user_updated_at = DateTime.UtcNow;
            user.user_updated_by = data.userId;

            await _userManager.UpdateAsync(user);
            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", user, 200, data.lang, false);
        }

        public async Task<CommonResponse> GetByIdAsync(string user_id, RequestMetadata data)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null || !user.user_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }

            var masterRole = await _masterRoleRepo.GetByIdAsync(user.user_fk_role_id);
            if(masterRole == null)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }
            var userResponse = _mapper.Map<AdminGetModel>(user);
            userResponse.user_role_name = masterRole.role_name;
            userResponse.user_role_permission = masterRole.role_permission;
            return _responseHelper.CreateResponse("Success", userResponse, 200, data.lang, false);
        }
        
        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _userRepo.PagingAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("PaginatedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _userRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }
        public async Task<CommonResponse> ChangePassword(ChangePasswordRequest request, RequestMetadata data)
        {
            // Fetch the record using repository
            var user = await _userManager.FindByIdAsync(request.user_id);

            // If the record is not found, return a proper response
            if (user == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            
            if (!result.Succeeded)
            {
                return _responseHelper.CreateResponse("InValidEmailPassword", false, 400, data.lang, false);
            }
         
            // Save changes using the repository update method
            await _userRepo.UpdateAsync(user);

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAdminAsync(UpdateAdminRequest request, RequestMetadata data)
        {
            // Fetch the record using repository
            var user = await _userManager.FindByIdAsync(request.user_id);

            // If the record is not found, return a proper response
            if (user == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            var masterRole = await _masterRoleRepo.GetFirstOrDefault(x => x.role_id == request.user_fk_role_id);
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RoleNotFound", false, 404, data.lang, false);
            }
            user.user_fk_role_id = request.user_fk_role_id;
            user.user_fk_role_name = masterRole.role_name;
            user.user_full_Name = request.user_full_Name;
            user.user_updated_by = data.userId;
            user.user_updated_at = DateTime.UtcNow;
           
            // Save changes using the repository update method
            await _userRepo.UpdateAsync(user);

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        public async Task<CommonResponse> DeleteAdminByIdAsync(string user_id, RequestMetadata data)
        {
            var user = await _userManager.FindByIdAsync(user_id);

            // If the record is not found, return a proper response
            if (user == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            
            if(user.user_is_owner)
            {
                return _responseHelper.CreateResponse("OwnerCannotDelete", null, 404, data.lang, false);
            }

            user.user_is_active = false;
            await _userRepo.UpdateAsync(user);
            return _responseHelper.CreateResponse("Success", true, 200, data.lang, false);

        }
        #endregion

        #region Master Role Api

        public async Task<CommonResponse> GetMasterRoleByIdAsync(int rl_id, RequestMetadata data)
        {
            // Fetch the record using repository
            var masterRole = await _masterRoleRepo.GetByIdAsync(rl_id);

            // If the record is not found, return a proper response
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            return _responseHelper.CreateResponse("Success", masterRole, 200, data.lang, false);

        }
        public async Task<CommonResponse> AddMasterRoleAsync(MasterRoleAddModel request, RequestMetadata data)
        {
            try
            {
                var masterRole = await _masterRoleRepo.GetFirstOrDefault(x => x.role_name == request.role_name);
                if (masterRole != null)
                {
                    return _responseHelper.CreateResponse("RecordAlreadyExist", null, 409, data.lang, false);
                }

                // Add and Mapp Data
                var responce = await _masterRoleRepo.AddAsync(_adminHelper.MapMasterRole(request, data));

                // Return success response
                return _responseHelper.CreateResponse("AddSuccessful", responce, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CommonResponse> UpdateMasterRoleAsync(MasterRoleUpdateModel request, RequestMetadata data)
        {
            // Fetch the record using repository
            var masterRole = await _masterRoleRepo.GetByIdAsync(request.role_id);

            // If the record is not found, return a proper response
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            string roleName = masterRole.role_name;
            masterRole.role_permission = request.role_permission;
            masterRole.role_name = request.role_name;
            masterRole.role_is_editable = request.role_is_editable;
            masterRole.role_is_super = request.role_is_super;
            masterRole.role_updated_at = DateTime.UtcNow;
            masterRole.role_updated_by = data.userId;

            // Save changes using the repository update method
            await _masterRoleRepo.UpdateAsync(masterRole);

            if(roleName != request.role_name)
            {
                var users = _userManager.Users.Where(x => x.user_fk_role_id == request.role_id).ToList();
                foreach (var user in users)
                {
                    user.user_fk_role_name = request.role_name;
                }
                await _userRepo.UpdateManyAsync(users);
            }

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }
        public async Task<CommonResponse> PagingMasterRoleAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _masterRoleRepo.PagingAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("PaginatedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> ExportMasterRoleAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _masterRoleRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("PaginatedDataSuccess", pagedResponse, 200, data.lang, false);
        }
        public async Task<CommonResponse> LookupMasterRoleAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var masterRole = await _masterRoleRepo.GetAllAsync();
            // If the record is not found, return a proper response
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<MasterRoleLookupModel>>(masterRole);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }

        public async Task<CommonResponse> DeleteMasterRoleByIdAsync(int rl_id, RequestMetadata data)
        {
            var users = _userManager.Users.Where(x => x.user_fk_role_id == rl_id && x.user_is_active).ToList();
            if(users.Count > 0)
            {
                return _responseHelper.CreateResponse("AdminAlreadyExist", null, 404, data.lang, true);
            }
            // Fetch the record using repository
            var masterRole = await _masterRoleRepo.GetByIdAsync(rl_id);

            // If the record is not found, return a proper response
            if (masterRole == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, true);
            }
            await _masterRoleRepo.DeleteAsync(masterRole);
            return _responseHelper.CreateResponse("Success", true, 200, data.lang, false);

        }
        #endregion
    }
}
