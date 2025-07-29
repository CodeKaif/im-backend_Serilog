using AutoMapper;
using IM.Notification.Repository.V1.CarCompanyRepository.Interface;
using IM.Notification.Service.V1.ApplicationLogsService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Notification.Service.V1.ApplicationLogsService.Impl
{
    public class ApplicationLogsSrvc : IApplicationLogsSrvc
    {

        private readonly ResponseHelper _responseHelper;
        private readonly IApplicationLogsRepo _applicationLogsRepo;

        public ApplicationLogsSrvc(IApplicationLogsRepo applicationLogsRepo, ResponseHelper responseHelper)
        {
            _applicationLogsRepo = applicationLogsRepo;
            _responseHelper = responseHelper;
            
        }
        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _applicationLogsRepo.PagingAsync(request, data);

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
            var response = await _applicationLogsRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }
    }
}
