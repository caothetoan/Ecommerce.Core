using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Vnit.ApplicationCore.Helpers
{
    public static class ValidateExtensionMethods
    {
        /// <summary>
        /// Tên người dùng từ 3 đến 20 ký tự bao gồm các ký tự a-z, chữ số và không chứa ký tự đặc biệt
        /// </summary>
        public static string UserNameExpression = @"^[a-zA-Z0-9_]{3,20}$";// //^(?=.{3,32}$)(?!.*[._-]{2})(?!.*[0-9]{5,})[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$
        /// <summary>
        /// Mật khẩu từ 8 đến 20 ký tự bao gồm các ký tự a-z 0-9. Mật khẩu phải chứa ít nhất một ký tự in hoa và một ký tự đặc biệt.
        /// </summary>
        public static string PasswordExpression = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$";
        /// <summary>
        /// Địa chỉ email bao gồm các ký tự a-z, chữ số
        /// </summary>
        public static string EmailExpression = "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$";

        /// <summary>
        /// Số điện thoại từ 10 đến 11 chữ số
        /// </summary>
        public static string PhoneExpression = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4,5})$";// @"^[0]\d{9,10}$";
        /// <summary>
        /// Số chứng minh thư từ 9 đến 12 ký tự
        /// </summary>
        public static string IdentityCardExpression = @"^\d{9,12}$";

        /// <summary>
        /// Kiểm tra chuỗi là null hoặc rỗng
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !String.IsNullOrEmpty(s);
        }

        public static bool IsGuidEmpty(this Guid guid)
        {
            return guid.Equals(Guid.Empty);
        }

        public static bool IsNotGuidEmpty(this Guid guid)
        {
            return !guid.Equals(Guid.Empty);
        }
        /// <summary>
        /// Validate UserName Rules are:
        ///usernames should start with[a - z]
        ///usernames should end with[a - z0 - 9]
        ///usernames can have a length between 3 and 32
        ///usernames can contain any of[a - z0 - 9\._ -]
        ///Numbers should not be in the vicinity of each other more than 4 times.I mean p1234 is a match and p12345 is not.
        ///    each username can contains only one of[\._ -]. I mean a username can contain.or - or _
        ///each., -, and _ should be followed by an alpha-numeric.I mean a.can not be followed by another .. They should not be in the vicinity of each other.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsValidUserName(this string userName)
        {
            if (String.IsNullOrEmpty(userName))
                return false;

            userName = userName.Trim();
            var result = Regex.IsMatch(userName, UserNameExpression, RegexOptions.IgnoreCase);
            return result;
        }
        /// <summary>
        /// Kiểm tra chuỗi có chứa mã HTML không
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsContainHtml(this string userName)
        {
            if (String.IsNullOrEmpty(userName))
                return false;

            userName = userName.Trim();
            var result = Regex.IsMatch(userName, @"<(.|\n)*?>", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// Kiểm tra mật khẩu hợp lệ từ 8 đến 20 ký tự bao gồm các ký tự a-z 0-9. Mật khẩu phải chứa ít nhất một ký tự in hoa và một ký tự đặc biệt.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsValidPassword(this string userName)
        {
            if (String.IsNullOrEmpty(userName))
                return false;

            userName = userName.Trim();
            var result = Regex.IsMatch(userName, PasswordExpression, RegexOptions.IgnoreCase);
            return result;
        }
        /// <summary>
        /// Kiểm tra số chứng minh thư hợp lệ.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool IsValidIdentityCard(this string card)
        {
            if (String.IsNullOrEmpty(card))
                return false;

            card = card.Trim();
            var result = Regex.IsMatch(card, IdentityCardExpression, RegexOptions.IgnoreCase);
            if (result)
                return card.Length == 9 || card.Length == 12;
            return result;
        }
        /// <summary>
        /// Kiểm tra email hợp lệ.
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(this string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, EmailExpression,
                RegexOptions.IgnoreCase);
            return result;
        }

        public static bool IsValidMobile(this string strPhoneNumber)
        {
            //            string matchPhoneNumberPattern = "^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

            //            string pattern = @"
            //^                  # From Beginning of line
            //(?:\(?)            # Match but don't capture optional (
            //(?<AreaCode>\d{3}) # 3 digit area code
            //(?:[\).\s]?)       # Optional ) or . or space
            //(?<Prefix>\d{3})   # Prefix
            //(?:[-\.\s]?)       # optional - or . or space
            //(?<Suffix>\d{4})   # Suffix
            //(?!\d)             # Fail if eleventh number found";
            //return Regex.IsMatch(strPhoneNumber, pattern);

            if (strPhoneNumber.IsNullOrEmpty())
                return false;

            string patternDienThoai = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4,5})$";// @"^[0]\d{9,10}$";

            Regex myRegexDienThoai = new Regex(patternDienThoai);

            Match mDienThoai = myRegexDienThoai.Match(strPhoneNumber);

            if (!mDienThoai.Success)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Verifies that string is an valid IP-Address
        /// </summary>
        /// <param name="ipAddress">IPAddress to verify</param>
        /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
        public static bool IsValidIpAddress(this string ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip);
        }

        public static bool IsValidDomainName(this string name)
        {
            return Uri.CheckHostName(name) != UriHostNameType.Unknown;
        }
    }
}
