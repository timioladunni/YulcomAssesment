using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YulcomAssesment.API.Data
{
    public class YulcomAssesmentContext : DbContext
    {
        public YulcomAssesmentContext(DbContextOptions<YulcomAssesmentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApiKey>().HasIndex(b => new { b.PublicApiKey, b.SecretApiKey });
            base.OnModelCreating(builder);
        }

        public virtual DbSet<ApiKey> ApiKeys { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<YulcomAssesmentData> YulcomAssesmentData { get; set; }
    }
}
