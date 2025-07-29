using IM.Domain.Entities.CarDomain;
using IM.Dto.V1.Car.DataModel;
using IM.Repository.Core.Generic.Interface;
using Infrastructure.V1.FilterBase.Model;
using Middleware.V1.Request.Model;

namespace IM.Repository.V1.CarRepository.Interface
{
    public interface ICarRepo : IGenericRepositoryAsync<Car>
    {
        Task<(IReadOnlyList<CarDto> Items, int TotalCount)> PagingAsync(FilterRequest filter, RequestMetadata metadata);
        Task<(IReadOnlyList<CarDto> Items, int TotalCount)> ExportAsync(FilterRequest filter, RequestMetadata metadata);
    }
}
