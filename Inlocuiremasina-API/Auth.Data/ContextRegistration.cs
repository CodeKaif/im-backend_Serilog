using Auth.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Data
{
    public class ContextRegistration
    {
        public static void ContextServices(IServiceCollection services, IConfiguration Configuration)
        {
            // AuthDBContext Registration
            services.AddDbContextPool<IMAuthDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IMAuthDBConnection"), b => b.MigrationsAssembly("Auth.Data")));

            // Configure Identity
            //services.AddIdentity<ApplicationUser, IdentityRole<int>>()
            //    .AddEntityFrameworkStores<IMAuthDbContext>()
            //    .AddDefaultTokenProviders();

        }
    }
}
