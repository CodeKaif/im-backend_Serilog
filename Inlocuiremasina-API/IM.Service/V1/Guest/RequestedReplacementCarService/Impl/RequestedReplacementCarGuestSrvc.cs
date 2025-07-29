using AutoMapper;
using Azure.Core;
using CommonApiCallHelper.V1.Constant;
using CommonApiCallHelper.V1.Interface;
using IM.Domain.Entities.CarDomain;
using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.EmailSendModel.Request;
using IM.Dto.V1.EmailSendModel.Responce;
using IM.Dto.V1.RequestedReplacementCarModel.Request;
using IM.Repository.V1.RequestedReplacementCarRepository.Interface;
using IM.Service.Constant;
using IM.Service.V1.Admin.CarService.Interface;
using IM.Service.V1.Admin.EmailRecipientService.Interface;
using IM.Service.V1.Admin.InsuranceCompanyService.Interface;
using IM.Service.V1.Guest.RequestedReplacementCarService.Helper;
using IM.Service.V1.Guest.RequestedReplacementCarService.Interface;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Guest.RequestedReplacementCarService.Impl
{
    public class RequestedReplacementCarGuestSrvc : IRequestedReplacementCarGuestSrvc
    {
        private readonly ResponseHelper _responseHelper;
        private readonly IRequestedReplacementCarRepo _requestedReplacementCarRepo;
        private readonly RequestedReplacementCarGuestHelper _requestedReplacementCarGuestHelper;
        private readonly ICommonApiCall _commonApiCall;
        private readonly IEmailRecipientSrvc _emailRecipientSrvc;
        private readonly IMapper _mapper;
        private readonly ICarSrvc _carSrvc;
        private readonly IInsuranceCompanySrvc _insuranceCompanySrvc;

        public RequestedReplacementCarGuestSrvc(IMapper mapper, IRequestedReplacementCarRepo requestedReplacementCarRepo, 
                                                ResponseHelper responseHelper, ICarSrvc carSrvc, IInsuranceCompanySrvc insuranceCompanySrvc,
                                                ICommonApiCall commonApiCall, IEmailRecipientSrvc emailRecipientSrvc)
        {
            _requestedReplacementCarRepo = requestedReplacementCarRepo;
            _responseHelper = responseHelper;
            _requestedReplacementCarGuestHelper = new RequestedReplacementCarGuestHelper();
            _commonApiCall = commonApiCall;
            _emailRecipientSrvc = emailRecipientSrvc;
            _mapper = mapper;
            _carSrvc = carSrvc;
            _insuranceCompanySrvc = insuranceCompanySrvc;
        }

        #region For Guest EndPoint
        public async Task<CommonResponse> AddAsync(AddRequestedReplacementCarRequest request, RequestMetadata data)
        {
            try
            {
                
                var insurance = await _insuranceCompanySrvc.GetInsuranceCompanyAsync(request.rrc_fk_ic_id, data);
                if (insurance == null)
                {
                    return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, true);
                }
                var requestCar = _requestedReplacementCarGuestHelper.MapRequestedReplacementCar(request, insurance, data);

                var responce = await _requestedReplacementCarRepo.AddAsync(requestCar);

                 await SendMail(new List<string> { request.rrc_email }, "RequestedReplacementCar", "Car Request Confirmation", responce, data);

                var emailRecipient = await _emailRecipientSrvc.GetFirstOrDefault(data);
                
                if (emailRecipient != null)
                {
                    var adminEmail = emailRecipient.er_email.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(email => email.Trim()).ToList();
                    await SendMail(adminEmail, "AdminRequestedReplacementCar", "New car replacement request", responce, data); 
                }

                // Return success response
                return _responseHelper.CreateResponse("AddSuccessful", responce, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion For Guest EndPoint

        public  async Task<Response<EmailSccessModel>> SendMail(List<string> email, string template,string subject, RequestedReplacementCar request, RequestMetadata metadata)
        {
            if(template == "RequestedReplacementCar" && metadata.lang == "ro")
            {
                subject = "Confirmarea solicitării";
            }
            
            EmailSendRequest emailSend = new EmailSendRequest
            {
                to = email,
                template = template,
                subject = subject,
                is_singal_mail = true,
                placeholders = new Dictionary<string, string>
                {
                     { "customer_name", request.rrc_fullname},
                      { "full_name", request.rrc_fullname},
                      { "phone_num", request.rrc_mobile},
                      { "location", request.rrc_pickup_location},
                      { "liscence_plate", request.rrc_license_plate},
                      { "car_model", request.rrc_fk_cr_model},
                      { "car_insuarance", request.rrc_fk_ic_name}
                },
                lang = metadata.lang
            };

            //_commonApiCall.FireForgotPost(emailSend, ServiceEndpointConstants.NotificationApi, ApiEndPointConstant.SendEmailAsync());
          return await  _commonApiCall.PostDataAsync<Response<EmailSccessModel>>(emailSend, ServiceEndpointConstants.NotificationApi, ApiEndPointConstant.SendEmailAsync());
        }
    }
}
