using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Constants
{
    public static class ConstantKey
    {
        public static string WorkingLanguage = "WorkingLanguage";

        public static string AccessTokenKey = "AccessTokenKey";
        public static string UserSessionKey = "UserSessionKey";
        public static string UserAdminSessionKey = "UserAdminSessionKey";



        public static string MediaSaveLocation = "MediaSaveLocation";
        public static string ImageServerUrl = "ImageServerUrl";
        public static string OtherMediaSavePath = "OtherMediaSavePath";
        public static string PictureSavePath = "PictureSavePath";
        public static string VideoSavePath = "VideoSavePath";

        public static int SummaryMaxLength = 150;
        public static string NotificationTempData = "ViettelIDC.notifications.{0}";

        public static string AutomationApiUrl = "AutomationApiUrl";

        public static string WebsiteApiUrl = "WebsiteApiUrl";

        public static string ApplicationFormUrlencoded = "application/x-www-form-urlencoded";

        public static string ApplicationJson = "application/json";

        public static string ModelStateIsNotValid = "Dữ liệu không hợp lệ";

        public static string WebsiteApiResonseIsNull = "Dữ liệu trả về lỗi";

        public static string PermissionDenied = "Bạn không có quyền thực hiện hành động này";

        public static string AccessDenied = "Truy cập bị từ chối";

        #region language Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        public const string LANGUAGES_BY_ID_KEY = "language.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        public const string LANGUAGES_ALL_KEY = "language.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        public const string LANGUAGES_PATTERN_KEY = "language.";


        public static string ViettelCacheStatic = "viettel_cache_static";

        public static string ViettelidcCachePerRequest = "viettelidc_cache_per_request";

        #endregion

        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// {2} : language ID
        /// </remarks>
        public const string URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY = "Viettelidc.urlrecord.active.id-name-language-{0}-{1}-{2}";
        /// <summary>
        /// Key for caching
        /// </summary>
        public const string URLRECORD_ALL_KEY = "Viettelidc.urlrecord.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : slug
        /// </remarks>
        public const string URLRECORD_BY_SLUG_KEY = "Viettelidc.urlrecord.active.slug-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        public const string URLRECORD_PATTERN_KEY = "Viettelidc.urlrecord.";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        public const string LOCALSTRINGRESOURCES_ALL_KEY = "Viettelidc.lsr.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// {1} : resource key
        /// </remarks>
        public const string LOCALSTRINGRESOURCES_BY_RESOURCENAME_KEY = "Viettelidc.lsr.{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        public const string LOCALSTRINGRESOURCES_PATTERN_KEY = "Viettelidc.lsr.";


        #endregion

        public static string CustomerCaptchaKey = "CustomerCaptchaKey";
        public static string AdminCaptchaKey = "AdminCaptchaKey";

        /// <summary>
        /// Có hiển thị mã bảo mật không
        /// </summary>
        public static string IsShowCaptcha = "IsShowCaptcha";

        /// <summary>
        /// Số lần đăng nhập sai lưu vào session
        /// </summary>
        public static string FailedLoginAttempts = "FailedLoginAttempts";

        /// <summary>
        /// Số lần cập nhật sai mật khẩu lưu vào session
        /// </summary>
        public static string FailedUpdatePasswordAttempts = "FailedUpdatePasswordAttempts";

        public static int TicketCodeNumber = 6;
        public static string TicketCodePrefix = "TK-";

        public static int OrderCodeNumber = 6;
        public static string OrderCodePrefix = "SO-";

        public static string Contact = "Liên hệ";

        public static string CurrencyUnit = "VNĐ";

        public static int ItemPerPage = 10;
        public static int ItemPerSlider = 5;

        public static int UserNameMinLength = 3;

        public static int UserNameMaxLength = 20;

        public static int FullNameMaxLength = 50;

        public static int PasswordMinLength = 8; // 6

        public static int PasswordMaxLength = 20;

        /// <summary>
        /// Nhập mật khẩu từ {0} đến {1} ký tự bao gồm các ký tự a-z 0-9. Mật khẩu phải chứa ít nhất một ký tự in hoa và một ký tự đặc biệt.
        /// </summary>
        public static string PasswordRuleMessage =
            string.Format(
                "Nhập mật khẩu từ {0} đến {1} ký tự bao gồm các ký tự a-z 0-9. Mật khẩu phải chứa ít nhất một ký tự in hoa và một ký tự đặc biệt.",
                PasswordMinLength, PasswordMaxLength);

        /// <summary>
        /// Mật khẩu không hợp lệ. Nhập mật khẩu từ {0} đến {1} ký tự bao gồm các ký tự a-z 0-9. Mật khẩu phải chứa ít nhất một ký tự in hoa và một ký tự đặc biệt.
        /// </summary>
        public static string PasswordInvalidMessage =
            string.Format(
                "Mật khẩu không hợp lệ. Nhập mật khẩu từ {0} đến {1} ký tự bao gồm các ký tự a-z 0-9. Mật khẩu phải chứa ít nhất một ký tự in hoa và một ký tự đặc biệt.",
                PasswordMinLength, PasswordMaxLength);

        public static string WebConfigPath => "~/web.config";

        /// <summary>
        /// Gets a request path to the install URL
        /// </summary>
        public static string InstallPath => "install";

        /// <summary>
        /// Gets a request path to the keep alive URL
        /// </summary>
        public static string KeepAlivePath => "keepalive/index";

        /// <summary>
        /// Gets the name of a request item that stores the value that indicates whether the client is being redirected to a new location using POST
        /// </summary>
        public static string IsPostBeingDoneRequestItem => "nop.IsPOSTBeingDone";

        /// <summary>
        /// Gets the name of HTTP_CLUSTER_HTTPS header
        /// </summary>
        public static string HttpClusterHttpsHeader => "HTTP_CLUSTER_HTTPS";

        /// <summary>
        /// Gets the name of HTTP_X_FORWARDED_PROTO header
        /// </summary>
        public static string HttpXForwardedProtoHeader => "X-Forwarded-Proto";

        /// <summary>
        /// Gets the name of X-FORWARDED-FOR header
        /// </summary>
        public static string XForwardedForHeader => "X-FORWARDED-FOR";

        public static string CorsPolicy = "SiteCorsPolicy";
    }
}
