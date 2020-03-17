﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Management;

namespace Vnit.Api.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring DB context on application startup
    /// </summary>
    public class VnitDbStartup : IVnitStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add object context
            services.AddVnitDbContext();

            //add EF services
            services.AddEntityFrameworkSqlServer();
            //services.AddEntityFrameworkProxies();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="app">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(ConstantKey.CorsPolicy);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            get { return 10; }
        }
    }
}
