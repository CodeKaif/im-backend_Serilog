using IM.Domain.Entities.PrivacyPolicyDomain;
using IM.Repository.Core.Generic.Interface;

namespace IM.Repository.V1.PrivacyPolicyRepository.Interface
{
    public interface IPrivacyPolicyRepo : IGenericRepositoryAsync<PrivacyPolicy>
    {
    }
}
