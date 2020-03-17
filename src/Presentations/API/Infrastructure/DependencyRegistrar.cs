using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vnit.ApplicationCore.Configuration;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Management;
using Vnit.ApplicationCore.Services;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.ApplicationCore.Services.Medias;
using Vnit.ApplicationCore.Services.Settings;
using Vnit.Infrastructure.Data;
using Vnit.Infrastructure.Logging;
using Vnit.Infrastructure.ThirdpartyServices;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Caching;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.ApplicationCore.Services.Security;
using Vnit.ApplicationCore.Services.Users;
using Vnit.Services.Catalogs;
using Vnit.Services.Customers;
using Vnit.Services.EntityProperties;
using Vnit.Services.Installation;
using Vnit.Services.Languages;
using Vnit.Services.Medias;
using Vnit.Services.News;
using Vnit.Services.Pages;
using Vnit.Services.Security;
using Vnit.Services.SEO;
using Vnit.Services.Skills;
using Vnit.Services.Users;
using Vnit.Services.VerboseReporter;
using Vnit.Services.Widgets;

namespace Catalog.API.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {

        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        /// <param name="configuration"></param>
        public virtual void Register(IServiceCollection services, ITypeFinder typeFinder, NopConfig config, IConfiguration configuration)
        {          
            services.AddScoped<IDatabaseContext, CatalogContext>();
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            #region services
            //services.AddScoped<ICatalogService, CachedCatalogService>();
            //services.AddScoped<IBasketService, BasketService>();
            //services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<CatalogService>();
            services.Configure<CatalogSettings>(configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();

            RegisterServices(services);

            #endregion

            // Add memory cache services
            services.AddMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

        }
        public void RegisterServices(IServiceCollection services)
        {
            //var asm = AssemblyLoader.LoadBinDirectoryAssemblies();

            ////to register services, we need to get all types from services assembly and register each of them;
            //var serviceAssembly = asm.First(x => x.FullName.Contains("Vnit.ApplicationCore.Services"));
            services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddScoped<ICryptographyService, CryptographyService>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IEmailAccountService, EmailAccountService>();
            services.AddScoped<IEmailMessageService, EmailMessageService>();

            services.AddScoped<IEntityPropertyService, EntityPropertyService>();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILocalizedPropertyService, LocalizedPropertyService>();
            services.AddScoped<ILocaleStringResourceService, LocaleStringResourceService>();
            services.AddScoped<IInstallationService, InstallationService>();

            services.AddScoped<IMediaService, MediaService>();

            services.AddScoped<INewsItemService, NewsItemService>();

            services.AddScoped<INewsCategoryService, NewsCategoryService>();

            services.AddScoped<INewsItemCategoryService, NewsItemCategoryService>();

            services.AddScoped<INewsItemTagService, NewsItemTagService>();

            services.AddScoped<INewsLetterSubscriptionService, NewsLetterSubscriptionService>();
            
            //services.AddScoped<INotificationEventService, NotificationEventService>();
            
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();
           
            services.AddScoped<IPageService, PageService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ISkillService, SkillService>();
          
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IUrlRecordService, UrlRecordService>();
            
            services.AddScoped<IVerboseReporterService, VerboseReporterService>();
           
            services.AddScoped<ISettingService, SettingService>();

            services.AddScoped<INopFileProvider, NopFileProvider>();
            services.AddScoped<IWebHelper, WebHelper>();
            services.AddScoped<IWidgetService, WidgetService>();
            
            services.AddScoped<HostingConfig>();
            services.AddScoped<LocalizationSettings>();
            services.AddScoped<SecuritySettings>();
            services.AddScoped<UserSettings>();

        }
        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }
}
