using Auth.Domain.Entities.ApplicationUserDomain;
using Auth.Domain.Entities.MasterRoleDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Context
{
    public class IMAuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public IMAuthDbContext(DbContextOptions<IMAuthDbContext> options) : base(options)
        {
        }

        DbSet<MasterRole> master_role { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Change Identity table names if required
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<IdentityRole<int>>().ToTable("AspNetRoles");
            builder.Entity<IdentityUserRole<int>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("AspNetUserTokens");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("AspNetRoleClaims");

            // Custom tables mapping
            builder.Entity<MasterRole>().ToTable("master_role");

        }
    }
}
