using IM.Notification.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Notification.Data
{
    public class ContextRegistration
    {
        public static void ContextServices(IServiceCollection services, IConfiguration Configuration)
        {
            // IMContentDbContext Registration
            services.AddDbContextPool<IMNotificationDbCotext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LoggingDb")));
        }
    }
}
