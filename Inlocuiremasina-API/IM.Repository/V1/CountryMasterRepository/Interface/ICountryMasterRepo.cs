using IM.Domain.Entities.CountryMasterDomain;
using IM.Repository.Core.Generic.Interface;

namespace IM.Repository.V1.CountryMasterRepository.Interface
{
    public interface ICountryMasterRepo : IGenericRepositoryAsync<CountryMaster>
    {
    }
}
