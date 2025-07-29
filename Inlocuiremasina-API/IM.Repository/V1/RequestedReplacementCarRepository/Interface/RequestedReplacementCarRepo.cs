using IM.Domain.Entities.RequestedReplacementCarDomain;
using IM.Dto.V1.RequestedReplacementCarModel.DataModel;
using IM.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.RequestedReplacementCarRepository.Interface
{
    public interface IRequestedReplacementCarRepo : IGenericRepositoryAsync<RequestedReplacementCar>
    {
        Task<(IReadOnlyList<RequestedReplacementCarDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<RequestedReplacementCarDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
