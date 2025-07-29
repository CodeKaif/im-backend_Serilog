using IM.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Data
{
    public class ContextRegistration
    {
        public static void ContextServices(IServiceCollection services, IConfiguration Configuration)
        {
            // IMContentDbContext Registration
            services.AddDbContextPool<IMContentDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IMContentDBConnection")));
        }
    }
}
