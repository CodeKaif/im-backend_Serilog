using IM.Notification.Domain.Entities.ApplicationLogsDomain;
using Microsoft.EntityFrameworkCore;

namespace IM.Notification.Data.Context
{
    public class IMNotificationDbCotext : DbContext
    {
        public IMNotificationDbCotext(DbContextOptions<IMNotificationDbCotext> options) : base(options)
        {
        }

        public DbSet<ApplicationLogs> ApplicationLogs { get; set; }
    }
}
