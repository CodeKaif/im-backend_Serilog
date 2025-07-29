using IM.Domain.Entities.PickupLocationDomain;
using IM.Dto.V1.PickupLocation.DataModel;
using IM.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.PickupLocationRepository.Interface
{
    public interface IPickupLocationRepo : IGenericRepositoryAsync<PickupLocation>
    {
        Task<(IReadOnlyList<PickupLocationDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<PickupLocationDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);

    }
}
