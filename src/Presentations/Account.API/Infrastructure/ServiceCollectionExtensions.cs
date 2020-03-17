using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Vnit.ApplicationCore.Configuration;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Management;
using Vnit.Infrastructure.Data;
using Vnit.Infrastructure.Identity;

namespace Vnit.Api.Infrastructure
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <returns>Configured service provider</returns>
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //add NopConfig configuration parameters
            services.ConfigureStartupConfig<NopConfig>(configuration.GetSection("Nop"));
            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));
            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            services.AddVnitAuthentication(configuration);

            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);
           
            return serviceProvider;
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds services required for anti-forgery support
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddAntiForgery(this IServiceCollection services)
        {
            //override cookie name
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.AntiforgeryCookie}";

                //whether to allow the use of anti-forgery cookies from SSL protected page on the other store pages which are not
                options.Cookie.SecurePolicy = DataSettingsManager.DatabaseIsInstalled && EngineContext.Current.Resolve<SecuritySettings>().ForceSslForAllPages
                    ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            });
        }

        /// <summary>
        /// Adds services required for application session state
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.SessionCookie}";
                options.Cookie.HttpOnly = true;

                //whether to allow the use of session values from SSL protected page on the other store pages which are not
                options.Cookie.SecurePolicy = DataSettingsManager.DatabaseIsInstalled && EngineContext.Current.Resolve<SecuritySettings>().ForceSslForAllPages
                    ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            });
        }

        /// <summary>
        /// Adds services required for themes support
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddThemes(this IServiceCollection services)
        {
            if (!DataSettingsManager.DatabaseIsInstalled)
                return;

            ////themes support
            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.ViewLocationExpanders.Add(new ThemeableViewLocationExpander());
            //});
        }

        /// <summary>
        /// Adds data protection services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddVnitDataProtection(this IServiceCollection services)
        {
            ////check whether to persist data protection in Redis
            //var nopConfig = services.BuildServiceProvider().GetRequiredService<NopConfig>();
            //if (nopConfig.RedisCachingEnabled && nopConfig.PersistDataProtectionKeysToRedis)
            //{
            //    //store keys in Redis
            //    services.AddDataProtection().PersistKeysToRedis(() =>
            //    {
            //        var redisConnectionWrapper = EngineContext.Current.Resolve<IRedisConnectionWrapper>();
            //        return redisConnectionWrapper.GetDatabase();
            //    }, NopCachingDefaults.RedisDataProtectionKey);
            //}
            //else
            //{
            //    var dataProtectionKeysPath = CommonHelper.DefaultFileProvider.MapPath("~/App_Data/DataProtectionKeys");
            //    var dataProtectionKeysFolder = new System.IO.DirectoryInfo(dataProtectionKeysPath);

            //    //configure the data protection system to persist keys to the specified directory
            //    services.AddDataProtection().PersistKeysToFileSystem(dataProtectionKeysFolder);
            //}
        }

        /// <summary>
        /// Adds authentication service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration"></param>
        public static void AddVnitAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            #region Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddCookie()
                    .AddJwtBearer(jwtBearerOptions =>
                    {
                        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateActor = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["Token:Issuer"],
                            ValidAudience = configuration["Token:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                                (configuration["Token:Key"]))
                        };
                    });

            #region OpenId Authentication
            ////set default authentication schemes
            //var authenticationBuilder = services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = NopAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = NopAuthenticationDefaults.ExternalAuthenticationScheme;
            //});

            //const string yourAuthorizationServerAddress = "accounts.google.com";
            //services.AddAuthentication().AddJwtBearer(options =>
            //{
            //    options.MetadataAddress = $"https://{yourAuthorizationServerAddress}/.well-known/openid-configuration";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        ValidIssuer = yourAuthorizationServerAddress
            //    };
            //    //options.Authority = "{yourAuthorizationServerAddress}";
            //    //options.Audience = "{yourAudience}";

            //    //// For example only! Don't store your shared keys as strings in code.
            //    //// Use environment variables or the .NET Secret Manager instead.
            //    //var sharedKey = new SymmetricSecurityKey(
            //    //    Encoding.UTF8.GetBytes(Configuration["SigningKey"]));

            //    //options.TokenValidationParameters = new TokenValidationParameters
            //    //{
            //    //    // Clock skew compensates for server time drift.
            //    //    // We recommend 5 minutes or less:
            //    //    ClockSkew = TimeSpan.FromMinutes(5),
            //    //    // Specify the key used to sign the token:
            //    //    IssuerSigningKey = signingKey,
            //    //    RequireSignedTokens = true,
            //    //    // Ensure the token hasn't expired:
            //    //    RequireExpirationTime = true,
            //    //    ValidateLifetime = true,
            //    //    // Ensure the token audience matches our audience value (default true):
            //    //    ValidateAudience = true,
            //    //    ValidAudience = "api://default",
            //    //    // Ensure the token was issued by a trusted authorization server (default true):
            //    //    ValidateIssuer = true,
            //    //    ValidIssuer = "https://nate-example.oktapreview.com/oauth2/default"
            //    //};
            //});  
            #endregion
            
            #endregion

            #region Setup CORS           
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            //corsBuilder.WithOrigins("http://localhost:808"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy(ConstantKey.CorsPolicy, corsBuilder.Build());
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Signin";
                options.LogoutPath = "/Account/Signout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                };
            });
            #endregion

            #region Add Cookie
            ////add main cookie authentication
            //authenticationBuilder.AddCookie(NopAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.Cookie.Name = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.AuthenticationCookie}";
            //    options.Cookie.HttpOnly = true;
            //    options.LoginPath = NopAuthenticationDefaults.LoginPath;
            //    options.AccessDeniedPath = NopAuthenticationDefaults.AccessDeniedPath;

            //    //whether to allow the use of authentication cookies from SSL protected page on the other store pages which are not
            //    options.Cookie.SecurePolicy = DataSettingsManager.DatabaseIsInstalled && EngineContext.Current.Resolve<SecuritySettings>().ForceSslForAllPages
            //        ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            //});

            ////add external authentication
            //authenticationBuilder.AddCookie(NopAuthenticationDefaults.ExternalAuthenticationScheme, options =>
            //{
            //    options.Cookie.Name = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.ExternalAuthenticationCookie}";
            //    options.Cookie.HttpOnly = true;
            //    options.LoginPath = NopAuthenticationDefaults.LoginPath;
            //    options.AccessDeniedPath = NopAuthenticationDefaults.AccessDeniedPath;

            //    //whether to allow the use of authentication cookies from SSL protected page on the other store pages which are not
            //    options.Cookie.SecurePolicy = DataSettingsManager.DatabaseIsInstalled && EngineContext.Current.Resolve<SecuritySettings>().ForceSslForAllPages
            //        ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            //}); 
            #endregion

            #region register and configure external authentication plugins now
            ////register and configure external authentication plugins now
            //var typeFinder = new WebAppTypeFinder();
            //var externalAuthConfigurations = typeFinder.FindClassesOfType<IExternalAuthenticationRegistrar>();
            //var externalAuthInstances = externalAuthConfigurations
            //    .Where(x => PluginManager.FindPlugin(x)?.Installed ?? true) //ignore not installed plugins
            //    .Select(x => (IExternalAuthenticationRegistrar)Activator.CreateInstance(x));

            //foreach (var instance in externalAuthInstances)
            //    instance.Configure(authenticationBuilder); 
            #endregion
        }

        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddNopMvc(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddMvc();

            //sets the default value of settings on MvcOptions to match the behavior of asp.net core mvc 2.1
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var nopConfig = services.BuildServiceProvider().GetRequiredService<NopConfig>();
            if (nopConfig.UseSessionStateTempDataProvider)
            {
                //use session-based temp data provider
                mvcBuilder.AddSessionStateTempDataProvider();
            }
            else
            {
                //use cookie-based temp data provider
                mvcBuilder.AddCookieTempDataProvider(options =>
                {
                    options.Cookie.Name = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.TempDataCookie}";

                    //whether to allow the use of cookies from SSL protected page on the other store pages which are not
                    options.Cookie.SecurePolicy = DataSettingsManager.DatabaseIsInstalled && EngineContext.Current.Resolve<SecuritySettings>().ForceSslForAllPages
                        ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
                });
            }

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            ////add custom display metadata provider
            //mvcBuilder.AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new NopMetadataProvider()));

            ////add custom model binder provider (to the top of the provider list)
            //mvcBuilder.AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new NopModelBinderProvider()));

            ////add fluent validation
            //mvcBuilder.AddFluentValidation(configuration =>
            //{
            //    configuration.ValidatorFactoryType = typeof(NopValidatorFactory);
            //    //implicit/automatic validation of child properties
            //    configuration.ImplicitlyValidateChildProperties = true;
            //});

            return mvcBuilder;
        }

        /// <summary>
        /// Register custom RedirectResultExecutor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddNopRedirectResultExecutor(this IServiceCollection services)
        {
            //we use custom redirect executor as a workaround to allow using non-ASCII characters in redirect URLs
            //services.AddSingleton<IActionResultExecutor<RedirectResult>, NopRedirectResultExecutor>();
        }

        /// <summary>
        /// Register base object context
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddVnitDbContext(this IServiceCollection services)
        {
            services.AddDbContext<CatalogContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServerWithLazyLoading(services, "CatalogConnection");
            });
            services.AddDbContext<AppIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServerWithLazyLoading(services, "IdentityConnection");
            });
           
        }

        /// <summary>
        /// Add and configure MiniProfiler service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddNopMiniProfiler(this IServiceCollection services)
        {
            //whether database is already installed
            if (!DataSettingsManager.DatabaseIsInstalled)
                return;

            //services.AddMiniProfiler(miniProfilerOptions =>
            //{
            //    //use memory cache provider for storing each result
            //    (miniProfilerOptions.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);

            //    //whether MiniProfiler should be displayed
            //    miniProfilerOptions.ShouldProfile = request =>
            //        EngineContext.Current.Resolve<StoreInformationSettings>().DisplayMiniProfilerInPublicStore;

            //    //determine who can access the MiniProfiler results
            //    miniProfilerOptions.ResultsAuthorize = request =>
            //        !EngineContext.Current.Resolve<StoreInformationSettings>().DisplayMiniProfilerForAdminOnly ||
            //        EngineContext.Current.Resolve<IPermissionService>().Authorize(StandardPermissionProvider.AccessAdminPanel);
            //}).AddEntityFramework();
        }
    }
}
