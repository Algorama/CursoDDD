using Kernel.Domain.Model.Enums;
using Kernel.Domain.Model.Settings;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Infra.Repositories
{
    public class KernelContext : DbContext
    {
        public AppSettings AppSettings { get; set; }

        public KernelContext(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (AppSettings.Context == Context.UnitTest)
            {
                optionsBuilder.UseInMemoryDatabase(AppSettings.NoSqlDbSettings.DatabaseName);
            }
            else
            {
                optionsBuilder.UseCosmos(
                   AppSettings.NoSqlDbSettings.AccountEndpoint,
                   AppSettings.NoSqlDbSettings.AccountKey,
                   AppSettings.NoSqlDbSettings.DatabaseName,
                   options => { }
                 );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KernelContext).Assembly);
        }
    }
}