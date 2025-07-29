using AutoMapper;
using IM.Domain.Entities.EmailRecipientDomain;
using IM.Dto.V1.EmailRecipientModel.DataModel;
using IM.Dto.V1.EmailRecipientModel.Request;
using IM.Repository.V1.EmailRecipientRepository.Interface;
using IM.Service.V1.Admin.EmailRecipientService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.EmailRecipientService.Impl
{
    public class EmailRecipientSrvc : IEmailRecipientSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly IEmailRecipientRepo _emailRecipientRepo;

        public EmailRecipientSrvc(IMapper mapper,
                                  IEmailRecipientRepo emailRecipientRepo,
                                  ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _emailRecipientRepo = emailRecipientRepo;
            _responseHelper = responseHelper;
        }

        public async Task<CommonResponse> GetAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var entity = await _emailRecipientRepo.GetFirstOrDefault(x => x.er_is_active);

            // If the record is not found, return a proper response
            if (entity == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var result = _mapper.Map<EmailRecipientDto>(entity);

            // Return success response
            return _responseHelper.CreateResponse("Success", result, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAsync(UpdateEmailRecipientRequest request, RequestMetadata data)
        {
            // Fetch the record using repository
            var entity = await _emailRecipientRepo.GetByIdAsync(request.er_id);

            // If the record is not found, return a proper response
            if (entity == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            entity.er_email = request.er_email;
            entity.er_updated_at = DateTime.UtcNow;
            entity.er_updated_by = data.userId;

            // Save changes using the repository update method
            await _emailRecipientRepo.UpdateAsync(entity);

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        #region For Internal Use 
        public async Task<EmailRecipient> GetFirstOrDefault(RequestMetadata data)
        {
            return await _emailRecipientRepo.GetFirstOrDefault(x => x.er_is_active);
        }
        #endregion
    }
}
