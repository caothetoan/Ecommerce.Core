using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Management;
using Vnit.ApplicationCore.Services.Emails;
using Vnit.ApplicationCore.Services.Security;
using Vnit.ApplicationCore.Services.Settings;
using Vnit.ApplicationCore.Services.Users;
using Vnit.Infrastructure.Data;
using Vnit.Infrastructure.Identity;
using Vnit.Services.Catalogs;
using Vnit.Services.Security;
using Vnit.Services.Users;

namespace Vnit.Services.Installation
{
    public class InstallationService : IInstallationService
    {
        public void Install()
        {
            //DatabaseManager.SetDbInitializer(new CreateOrUpdateTables<DatabaseContext>());

            //run the migrator to install the database
            //MigrationManager.UpdateDatabaseToLatestVersion();

            ////any post installation tasks?
            //if (PostInstallationTasks.HasPostInstallationTasks())
            //    PostInstallationTasks.Execute();

            ////mark tables installed
            //ApplicationHelper.MarkTablesInstalled();


            //var catalogContext = EngineContext.Current.Resolve<CatalogContext>();

            ////// ===== Create tables ======
            ////catalogContext.Database.EnsureCreated();
            ////catalogContext.Database.MigrateAsync();

            //var loggerFactory = EngineContext.Current.Resolve<ILoggerFactory>();
            //CatalogContextSeed.SeedAsync(catalogContext, loggerFactory).Wait();

            //var appIdentityDbContext = EngineContext.Current.Resolve<AppIdentityDbContext>();

            ////// ===== Create tables ======
            ////appIdentityDbContext.Database.EnsureCreated();
            //// add default user
            //var userManager = EngineContext.Current.Resolve<UserManager<ApplicationUser>>();

            //AppIdentityDbContextSeed.SeedAsync(userManager).Wait();
            //// Adding Roles
            //var roleManager = EngineContext.Current.Resolve<RoleManager<IdentityRole>>();
            //AppIdentityDbContextSeed.InitializeRoles(roleManager).Wait();
        }

        public void Install(string connectionString, string providerName)
        {
            Install();
        }


        public void FillRequiredSeedData(string defaultUserEmail, string defaultUserPassword, string installDomain)
        {
            //first the settings

            SeedSettings(installDomain);

            //seed the roles
            SeedRoles();

            //then the user
            SeedDefaultUser(defaultUserEmail, defaultUserPassword);

            //seed email account
            SeedEmailAccount(installDomain);

            //seed email templates
            SeedEmailTemplates(defaultUserEmail, installDomain);

            //notification emails
            SeedNotificationEvents();

            SeedProducts();
            //update config file
            UpdateWebConfig();
        }

        /// <summary>
        /// Seed roles
        /// </summary>
        private void SeedRoles()
        {
            var roleService = EngineContext.Current.Resolve<IRoleService>();

            roleService.Insert(new Role()
            {
                RoleName = SystemRoleNames.Administrator,
                IsSystemRole = true,
                IsActive = true,
                SystemName = SystemRoleNames.Administrator
            });

            roleService.Insert(new Role()
            {
                RoleName = SystemRoleNames.Registered,
                IsSystemRole = true,
                IsActive = true,
                SystemName = SystemRoleNames.Registered
            });

            roleService.Insert(new Role()
            {
                RoleName = SystemRoleNames.Visitor,
                IsSystemRole = true,
                IsActive = true,
                SystemName = SystemRoleNames.Visitor
            });

        }

        /// <summary>
        /// Seed default user
        /// </summary>
        private void SeedDefaultUser(string email, string password)
        {
            var userRegistrationService = EngineContext.Current.Resolve<IUserRegistrationService>();
            var securitySettings = EngineContext.Current.Resolve<UserSettings>();

            var registrationResult = userRegistrationService.Register(email, password, securitySettings.PasswordFormat);
            if (registrationResult == UserRegistrationStatus.Success)
            {
                //add roles
                var roleService = EngineContext.Current.Resolve<IRoleService>();
                var userService = EngineContext.Current.Resolve<IUserService>();

                //first get user entity and assign administrator role
                var user = userService.FirstOrDefault(x => x.Email == email);
                if (user != null)
                    roleService.AssignRoleToUser(SystemRoleNames.Administrator, user);

            }
            else
            {
                throw new Exception("Installation failed");
            }
        }

        /// <summary>
        /// Seed settings
        /// </summary>
        private void SeedSettings(string installDomain)
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();

            //general settings
            settingService.Save(new GeneralSettings()
            {
                ImageServerDomain = "//" + string.Concat(installDomain, "/api"),
                VideoServerDomain = "//" + string.Concat(installDomain, "/api"),
                AutomationApiUrl = string.Concat(installDomain, "/api"),
                WebsiteApiUrl = string.Concat(installDomain, "/api"),
                Domain = installDomain,
                ApplicationCookieDomain = installDomain,
                IsInstalled = true
            });

            //media settings
            settingService.Save(new MediaSettings()
            {
                ThumbnailPictureSize = "100x100",
                SmallProfilePictureSize = "64x64",
                MediumProfilePictureSize = "128x128",
                SmallCoverPictureSize = "300x50",
                MediumCoverPictureSize = "800x300",
                PictureSaveLocation = MediaSaveLocation.FileSystem,
                PictureSavePath = "~/Content/Media/Uploads/Images",
                VideoSavePath = "~/Content/Media/Uploads/Videos",
                OtherMediaSaveLocation = MediaSaveLocation.FileSystem,
                OtherMediaSavePath = "~/Content/Media/Uploads/Others",
                DefaultUserProfileImageUrl = "~/Content/Media/d_male.jpg",
                DefaultUserProfileCoverUrl = "~/Content/Media/d_cover.jpg",
            });

            //user settings
            settingService.Save(new UserSettings()
            {
                UserRegistrationDefaultMode = RegistrationMode.WithActivationEmail,
                UserLinkTemplate = "<a href='' data-uid='{0}'>{1}</a>",
                ActivationPageUrl = installDomain + "/activate",
                DefaultPasswordStorageFormat = PasswordFormat.Sha1Hashed
            });


        }

        /// <summary>
        /// Seed email accounts
        /// </summary>
        private void SeedEmailAccount(string installDomain)
        {
            var emailAccountService = EngineContext.Current.Resolve<IEmailAccountService>();
            emailAccountService.Insert(new EmailAccount()
            {
                Email = "mailer@" + installDomain,
                DisplayName = "Ecommerce",
                Host = "smtp." + installDomain,
                Port = 485,
                IsDefault = true,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Username = "mailer@" + installDomain,
                Password = "password"
            });
        }

        private void SeedEmailTemplates(string adminEmail, string installDomain)
        {
            var emailAccountService = EngineContext.Current.Resolve<IEmailAccountService>();
            var emailTemplateService = EngineContext.Current.Resolve<IEmailTemplateService>();
            //var installEmailTemplatesPath =
            //    ServerHelper.GetLocalPathFromRelativePath("~/App_Data/InstallData/EmailTemplates/");
            //CommonHelper.MapPath("~/App_Data/InstallData/EmailTemplates/")
            var installEmailTemplatesPath =
                ("~/App_Data/InstallData/EmailTemplates/");

            //add email account
            var emailAccount = new EmailAccount()
            {
                Email = "support@" + installDomain,
                DisplayName = "Ecommerce",
                Host = "smtp.gmail.com",
                IsDefault = false,
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
                Username = "haylamvietnam@gmail.com",
                Password = "123456@A"
            };
            emailAccountService.Insert(emailAccount);

            var masterTemplate = new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = true,
                Subject = "Master Template",
                TemplateSystemName = EmailTemplateNames.Master,
                Name = "Master",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.Master)
            };
            emailTemplateService.Insert(masterTemplate);

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been created",
                TemplateSystemName = EmailTemplateNames.UserRegisteredMessage,
                Name = "User Registered",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserRegisteredMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "A new user has registered",
                TemplateSystemName = EmailTemplateNames.UserRegisteredMessageToAdmin,
                Name = "User Registered Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserRegisteredMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been activated",
                TemplateSystemName = EmailTemplateNames.UserActivatedMessage,
                Name = "User Activated",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserActivatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Activate your account",
                TemplateSystemName = EmailTemplateNames.UserActivationLinkMessage,
                Name = "User Activation Link",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserActivationLinkMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "We have received a password reset request",
                TemplateSystemName = EmailTemplateNames.PasswordRecoveryLinkMessage,
                Name = "Password Recovery Link",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.PasswordRecoveryLinkMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your password has been changed",
                TemplateSystemName = EmailTemplateNames.PasswordChangedMessage,
                Name = "Password Changed",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.PasswordChangedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been deactivated",
                TemplateSystemName = EmailTemplateNames.UserDeactivatedMessage,
                Name = "User Account Deactivated",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserDeactivatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been deactivated",
                TemplateSystemName = EmailTemplateNames.UserDeactivatedMessageToAdmin,
                Name = "User Account Deactivated Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template =
                    ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserDeactivatedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been deleted",
                TemplateSystemName = EmailTemplateNames.UserAccountDeletedMessage,
                Name = "User Account Deleted",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserAccountDeletedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "A user account has been deleted",
                TemplateSystemName = EmailTemplateNames.UserAccountDeletedMessageToAdmin,
                Name = "User Account Deleted Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template =
                    ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserAccountDeletedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });
        }


        private void SeedNotificationEvents()
        {
            //var notificationEventService = EngineContext.Current.Resolve<INotificationEventService>();
            ////get all events from notification event class. use reflection for easy insert
            //var fieldInfos = typeof(NotificationEventNames).GetFields(BindingFlags.Public | BindingFlags.Static);
            //foreach (var fi in fieldInfos)
            //{
            //    if (!fi.IsLiteral || fi.IsInitOnly)
            //        continue;
            //    //it's a constant
            //    var eventName = fi.GetRawConstantValue().ToString();
            //    notificationEventService.Insert(new NotificationEvent()
            //    {
            //        EventName = eventName,
            //        Enabled = true
            //    });
            //}
        }

        /// <summary>
        /// Seed roles
        /// </summary>
        private void SeedProducts()
        {
            //var categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var productService = EngineContext.Current.Resolve<IProductService>();

            var category = new Category()
            {
                Name = "Laptop",
                CreatedOnUtc = DateTime.Now,
                Published = true,
                DisplayOrder = 1,

            };
            //categoryService.Insert(category);
            var product = new Product()
            {
                Name = "Macbook Pro",
                CreatedOnUtc = DateTime.Now,
                Published = true,
                Price = 30000000
            };
            productService.Insert(product);

            productService.AttachProductToCategory(product, category);

        }


        #region Helper
        private string ReadEmailTemplate(string path, string templateName)
        {
            var filePath = path + templateName + ".html";
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }

        /// <summary>
        /// Update the webconfig file with required settings
        /// </summary>
        private void UpdateWebConfig()
        {
            try
            {
                var applicationConfiguration = EngineContext.Current.Resolve<IApplicationConfiguration>();
                var cryptographyService = EngineContext.Current.Resolve<ICryptographyService>();
                var key = cryptographyService.GetRandomPassword();
                var salt = cryptographyService.GetRandomPassword();

                applicationConfiguration.SetSetting("encryptionKey", key);
                applicationConfiguration.SetSetting("encryptionSalt", salt);
            }
            catch (Exception)
            {
                //an error occured while modifying config file, may be it's write protected or test mode is on?
            }
        }
        #endregion
    }
}
