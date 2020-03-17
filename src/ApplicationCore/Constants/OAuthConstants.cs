namespace Vnit.ApplicationCore.Constants
{
    public class OAuthConstants
    {
        public const string OAuthBase = "/oauth2";

        public const string TokenEndPointPath = OAuthBase + "/token";

        public const string AuthorizeEndPointPath = OAuthBase + "/authorize";

        public const string LoginPath = "/login";

        public const string LogoutPath = "/logout";

        public const int AccessTokenExpirationSeconds = 1800;

        public const int RefreshTokenExpirationSeconds = 1200;

        public const string Application = ".AspNet.Application";

        public const string AuthenticationType = ".AspNet.Application";

        public const string ApplicationCookie = ".AspNet.ApplicationCookie";

        public const string Bearer = "Bearer";

        public const string External = "External";

        public const string ExternalCookie = "ExternalCookie";
        
        public const string ApplicationCookieName = ".AspNet.ApplicationCookie";// "VIETTEL_IDC_SSO_COOKIE";

        public const string OwinChallengeFlag = "X-Challenge";
    }
}
