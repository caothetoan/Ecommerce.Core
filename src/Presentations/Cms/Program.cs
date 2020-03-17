using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Vnit.Infrastructure.Data;
using Vnit.Infrastructure.Identity;

namespace Vnit.Cms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                        .Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //    try
            //    {
            //        var catalogContext = services.GetRequiredService<CatalogContext>();

            //        //// ===== Create tables ======
            //        //catalogContext.Database.EnsureCreated();
            //        //catalogContext.Database.MigrateAsync();

            //        //var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //        CatalogContextSeed.SeedAsync(catalogContext, loggerFactory).Wait();

            //        //var appIdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            //        //// ===== Create tables ======
            //        //appIdentityDbContext.Database.EnsureCreated();
            //        // add default user
            //        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            //        AppIdentityDbContextSeed.SeedAsync(userManager).Wait();
            //        // Adding Roles
            //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //        AppIdentityDbContextSeed.InitializeRoles(roleManager).Wait();
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = loggerFactory.CreateLogger<Program>();
            //        logger.LogError(ex, "An error occurred seeding the DB.");
            //    }
            //}

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5107")
                .UseStartup<Startup>();
    }
}
