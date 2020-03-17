using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services;
using Vnit.Infrastructure.Data;
using Vnit.Infrastructure.Identity;
using Vnit.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vnit.RazorPages.Interfaces;
using Vnit.RazorPages.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Services.Caching;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.ApplicationCore.Services.Medias;
using Vnit.ApplicationCore.Services.Security;
using Vnit.ApplicationCore.Services.Settings;
using Vnit.ApplicationCore.Services.Users;
using Vnit.Infrastructure.ThirdpartyServices;
using Vnit.Services.Catalogs;
using Vnit.Services.Customers;
using Vnit.Services.EntityProperties;
using Vnit.Services.Installation;
using Vnit.Services.Languages;
using Vnit.Services.Medias;
using Vnit.Services.News;
using Vnit.Services.Orders;
using Vnit.Services.Pages;
using Vnit.Services.Security;
using Vnit.Services.SEO;
using Vnit.Services.Skills;
using Vnit.Services.VerboseReporter;
using Vnit.Services.Widgets;

namespace Vnit.Cms
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureTestingServices(services);

            // use real database
            ConfigureProductionServices(services);

        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<CatalogContext>(c =>
                c.UseInMemoryDatabase("Catalog"));

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            services.AddDbContext<CatalogContext>(dbContextOptionsBuilder =>
            {               
                    // Requires LocalDB which can be installed with SQL Server Express 2016
                    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
                    dbContextOptionsBuilder
                        .UseSqlServer(Configuration.GetConnectionString("CatalogConnection"),
                        b => b.MigrationsAssembly("Vnit.Cms"));
                
            });

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Signin";
                options.LogoutPath = "/Account/Signout";
            });
            services.AddScoped<IDatabaseContext, CatalogContext>();
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));            
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            #region services

            RegisterServices(services);

            services.AddScoped<ICatalogService, CachedCatalogService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CatalogService>();
            services.Configure<CatalogSettings>(Configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>()));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();

            #endregion

            // Add memory cache services
            services.AddMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Order");
                    options.Conventions.AuthorizePage("/Basket/Checkout");
                });

            _services = services;
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc();
        }

        public void RegisterServices(IServiceCollection services)
        {
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


            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IPageService, PageService>();

            services.AddScoped<ISkillService, SkillService>();

            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlRecordService, UrlRecordService>();

            services.AddScoped<IVerboseReporterService, VerboseReporterService>();
            services.AddScoped<IWidgetService, WidgetService>();

            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<LocalizationSettings>();

        }
    }
}
