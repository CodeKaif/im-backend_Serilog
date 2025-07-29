using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.RequestedReplacementCarModel.Request;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.RequestedReplacementCarService.Interface
{
    public interface IRequestedReplacementCarSrvc
    {
        Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data);
        Task<CommonResponse> ResendAdminMail(ResendMailRequest request, RequestMetadata data);
        Task<CommonResponse> ResendUserMail(ResendMailRequest request, RequestMetadata data);
        Task<RequestedReplacementCar> GetRequestedReplacementCarAsync(int rrc_id, RequestMetadata data);
        Task<bool> UpdateInsuranceCompanyAsync(int cc_id, string icName, RequestMetadata data);
    }
}
