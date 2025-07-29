using IM.Data.Context;
using IM.Domain.Entities.EmailRecipientDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.EmailRecipientRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.EmailRecipientRepository.Impl
{
    public class EmailRecipientRepo : GenericRepositoryAsync<EmailRecipient>, IEmailRecipientRepo
    {
        private readonly DbSet<EmailRecipient> _emailRecipient;

        public EmailRecipientRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _emailRecipient = dbContext.Set<EmailRecipient>();
        }
    }
}
