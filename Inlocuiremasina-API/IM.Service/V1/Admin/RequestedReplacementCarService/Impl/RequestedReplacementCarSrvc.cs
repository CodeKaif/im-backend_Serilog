using AutoMapper;
using CommonApiCallHelper.V1.Constant;
using CommonApiCallHelper.V1.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using EmailGateway.V1.Model;
using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.EmailSendModel.Responce;
using IM.Dto.V1.RequestedReplacementCarModel.Request;
using IM.Repository.V1.RequestedReplacementCarRepository.Interface;
using IM.Service.Constant;
using IM.Service.V1.Admin.EmailRecipientService.Interface;
using IM.Service.V1.Admin.RequestedReplacementCarService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.RequestedReplacementCarService.Impl
{
    public class RequestedReplacementCarSrvc : IRequestedReplacementCarSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestedReplacementCarRepo _requestedReplacementCarRepo;
        private readonly ICommonApiCall _commonApiCall;
        private readonly IEmailRecipientSrvc _emailRecipientSrvc;
        public RequestedReplacementCarSrvc(IMapper mapper, IRequestedReplacementCarRepo requestedReplacementCarRepo, ResponseHelper responseHelper, ICommonApiCall commonApiCall, IEmailRecipientSrvc emailRecipientSrvc)
        {
            _mapper = mapper;
            _requestedReplacementCarRepo = requestedReplacementCarRepo;
            _responseHelper = responseHelper;
            _commonApiCall = commonApiCall;
            _emailRecipientSrvc = emailRecipientSrvc;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _requestedReplacementCarRepo.PagingAsync(request, data);

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
            var response = await _requestedReplacementCarRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> ResendUserMail(ResendMailRequest request, RequestMetadata data)
        {
            try
            {
                var requestCar = await _requestedReplacementCarRepo.GetByIdAsync(request.rrc_id);
                if (requestCar == null)
                {
                    //Requested Replacement Car not found.
                    return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, true);
                }

                SendMail(new List<string> { requestCar.rrc_email }, "RequestedReplacementCar", "Car Request Confirmation", requestCar, data);
               
                    requestCar.rrc_user_mail_sent = true;
                    requestCar.rrc_updated_by = data.userId;
                    requestCar.rrc_updated_at = DateTime.UtcNow;
                    await _requestedReplacementCarRepo.UpdateAsync(requestCar);
                

                // Return success response
                return _responseHelper.CreateResponse("EmailSendSuccess", true, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CommonResponse> ResendAdminMail(ResendMailRequest request, RequestMetadata data)
        {
            try
            {
                var requestCar = await _requestedReplacementCarRepo.GetByIdAsync(request.rrc_id);
                if (requestCar == null)
                {
                    //Requested Replacement Car not found.
                    return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, true);
                }
                var emailRecipient = await _emailRecipientSrvc.GetFirstOrDefault(data);
                if (emailRecipient == null)
                {
                    //Email Recipient not found.
                    return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, true);
                }

                var adminEmail = emailRecipient.er_email.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(email => email.Trim()).ToList();

               SendMail(adminEmail, "AdminRequestedReplacementCar", "New car replacement request", requestCar, data);
                
                    requestCar.rrc_admin_mail_sent = true;
                    requestCar.rrc_updated_by = data.userId;
                    requestCar.rrc_updated_at = DateTime.UtcNow;
                    await _requestedReplacementCarRepo.UpdateAsync(requestCar);
               

                // Return success response
                return _responseHelper.CreateResponse("EmailSendSuccess", true, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion For Admin EndPoint
        public async Task<Response<EmailSccessModel>> SendMail(List<string> email, string template, string subject, RequestedReplacementCar request, RequestMetadata data)
        {
            if (template == "RequestedReplacementCar" && data.lang == "ro")
            {
                subject = "Confirmarea solicitării";
            }
            EmailSendRequest emailSend = new EmailSendRequest
            {
                to = email,
                template = template,
                subject = subject,
                is_singal_mail = true,
                lang = data.lang,
                placeholders = new Dictionary<string, string>
                  {
                      { "customer_name", request.rrc_fullname},
                      { "full_name", request.rrc_fullname},
                      { "phone_num", request.rrc_mobile},
                      { "location", request.rrc_pickup_location},
                      { "liscence_plate", request.rrc_license_plate},
                      { "car_model", request.rrc_fk_cr_model},
                      { "car_insuarance", request.rrc_fk_ic_name}
                  }

            };

            return await _commonApiCall.PostDataAsync<Response<EmailSccessModel>>(emailSend, ServiceEndpointConstants.NotificationApi, ApiEndPointConstant.SendEmailAsync());
        }

        #region internal use
        public async Task<RequestedReplacementCar> GetRequestedReplacementCarAsync(int rrc_id, RequestMetadata data)
        {
            return await _requestedReplacementCarRepo.GetFirstOrDefault(x => x.rrc_id == rrc_id && x.rrc_lang == data.lang && x.rrc_is_active);
        }

        public async Task<bool> UpdateInsuranceCompanyAsync(int ic_id, string icName, RequestMetadata data)
        {
            var requestedcar = await _requestedReplacementCarRepo.GetByConditionsAsync(x => x.rrc_lang == data.lang && x.rrc_fk_ic_id == ic_id);
            if (requestedcar == null || !requestedcar.Any())
            {
                return false;
            }
            foreach (var car in requestedcar)
            {
                car.rrc_fk_ic_name = icName;
            }

            await _requestedReplacementCarRepo.UpdateManyAsync(requestedcar.ToList());

            return true;
        }
        #endregion internal use
    }
}
