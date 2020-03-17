using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vnit.Api.Infrastructure
{
    /// <summary>
    /// Represents extensions of DbContextOptionsBuilder
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// SQL Server specific extension method for Microsoft.EntityFrameworkCore.DbContextOptionsBuilder
        /// </summary>
        /// <param name="optionsBuilder">Database context options builder</param>
        /// <param name="services">Collection of service descriptors</param>
        public static void UseSqlServerWithLazyLoading(this DbContextOptionsBuilder optionsBuilder, IServiceCollection services, string connectionName)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            //var nopConfig = services.BuildServiceProvider().GetRequiredService<NopConfig>();

            //var dataSettings = DataSettingsManager.LoadSettings();
            //if (!dataSettings?.IsValid ?? true)
            //    return;
            //Microsoft.EntityFrameworkCore.Proxies, Version=2.1.1.
            //var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();

            //if (nopConfig.UseRowNumberForPaging)
            //    dbContextOptionsBuilder.UseSqlServer(dataSettings.DataConnectionString, option => option.UseRowNumberForPaging());
            //else
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionName));
        }
    }
}
