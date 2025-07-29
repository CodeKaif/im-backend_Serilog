using IM.Repository.V1.EmailRecipientRepository.Impl;
using IM.Repository.V1.EmailRecipientRepository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Repository.V1.EmailRecipientRepository.Extension
{
    public class EmailRecipientExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEmailRecipientRepo, EmailRecipientRepo>();
        }
    }
}
