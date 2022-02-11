using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Analytics.Data.Domain.FacebookDatas;
using Analytics.Data.Domain.SocialNetworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Analytics.Data.Context
{

    public class AnalyticsDbContext : DbContext
    {

        public virtual DbSet<FacebookData> FacebookData { get; set; }
        public virtual DbSet<SocialNetwork> SocialNetwork { get; set; }
        public AnalyticsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("als");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
