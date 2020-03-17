using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Settings
{
    public class UserSettings : ISettings
    {
        /// <summary>
        /// Default registration mode for users
        /// </summary>
        public RegistrationMode UserRegistrationDefaultMode { get; set; }

        /// <summary>
        /// Default registration mode for users
        /// </summary>
        public PasswordFormat PasswordFormat { get; set; }

        public int SaltLength { get; set; }

        /// <summary>
        /// Specifies if user names are enabled for site
        /// </summary>
        public bool AreUserNamesEnabled { get; set; }

        /// <summary>
        /// Specifies if an email is also required to activate the user along with activation code
        /// </summary>
        public bool RequireEmailForUserActivation { get; set; }

        /// <summary>
        /// Specifies the html template used for creating user links within content
        /// </summary>
        public string UserLinkTemplate { get; set; }

        /// <summary>
        /// Specifies minimum length of string required to search users #imported from old webapi plugin
        /// </summary>
        public int PeopleSearchTermMinimumLength { get; set; }

        public string ActivationPageUrl { get; set; }

        public string PasswordResetPageUrl { get; set; }

        /// <summary>
        /// Số lần đăng nhập sai tối đa ( mặc định 5 )
        /// </summary>
        public int MaxFailedLoginAttempts { get; set; }

        public PasswordFormat DefaultPasswordStorageFormat { get; set; }
    }
}
