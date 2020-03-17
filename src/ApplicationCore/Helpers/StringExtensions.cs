using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Vnit.ApplicationCore.Helpers
{
    public static class StringExtensions
    {
        public static string JoinTags(this IEnumerable<string> items)
        {
            return String.Join("-", items ?? new[] { String.Empty });
        }

        public static string FormatAsCSV<T>(this IEnumerable<T> value) where T : class
        {
            StringBuilder stringBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            stringBuilder.AppendLine(String.Join(",", properties.Select(p => p.Name)));
            foreach (var csvLine in value)
            {
                var columnValues = properties
                    .Select(p => p.GetValue(csvLine)?.ToString())
                    .Select(p => p.FormatAsCSV());

                stringBuilder.AppendLine(String.Join(",", columnValues));
            }
            return stringBuilder.ToString();
        }

        public static bool IsValidCSV(this string value)
        {
            return value.IndexOfAny(new char[] { '"', ',' }) == -1;
        }

        public static string FormatAsCSV(this string value)
        {
            return String.IsNullOrEmpty(value) ?
                String.Empty :
                (
                    value.IsValidCSV() ?
                        value :
                        String.Format("\"{0}\"", value.Replace("\"", "\"\""))
                );
        }

        private const string UnsignChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        private const string UnicodeChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";


        /// <summary>
        /// Chuyển chuối tiếng việt về không dấu và thêm gạch - giữa hai từ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string UrlRewriting(this string title, string action, int id)
        {
            var retVal = string.Format("/{0}/{1}.{2}/", action, UrlRewriting(title), id);

            return retVal;
        }


        /// <summary>
        /// Chuyển đổi chuỗi ký tự có dấu sang chuỗi ký tự không dấu.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToUnsign(this string str)
        {
            var retVal = string.Empty;
            if (str == null)
                return retVal;
            for (var i = 0; i < str.Length; i++)
            {
                var pos = UnicodeChars.IndexOf(str[i].ToString(), StringComparison.Ordinal);
                if (pos >= 0)
                    retVal += UnsignChars[pos];
                else
                    retVal += str[i];
            }
            return retVal.TrimEnd().Replace(" ", "-");
        }

        /// <summary>
        /// Chuyển chuối tiếng việt về không dấu và thêm gạch - giữa hai từ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UrlRewriting(this string s)
        {
            var retVal = "";
            if (!string.IsNullOrEmpty(s))
            {
                //Chuyển thành không dấu và thêm ký tự '-' ở mỗi từ
                retVal = ConvertToUrlStandard(s);
                //Xóa ký tự đặc biệt
                retVal = ReplaceSpecialChar(retVal.TrimEnd().ToLower());
                //Lấy tối đa 100 ký tự
                retVal = GetSubString(retVal, 100, "");
            }
            if (retVal == "" || retVal == "con") retVal = "title";
            return retVal;
        }
        /// <summary>
        /// Chuyển thành không dấu và thêm ký tự '-' ở mỗi từ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertToUrlStandard(this string input)
        {
            var tmp = UnicodeToUnsign(input);
            var re = new Regex("[^a-zA-Z0-9_]");
            tmp = re.Replace(tmp, "_");
            return tmp;
        }
        /// <summary>
        /// Xóa ký tự đặc biệt trong chuỗi
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChar(this string src)
        {
            var specialChars = " ,~,`,!,@,#,$,%,^,&,*,(,),<,>,?,:,|,+,{,},\\,/,\",;,.,\r,\n,\t,=,_,[,]";
            var arrSc = specialChars.Split(',');
            for (int i = 0; i < arrSc.Length; i++)
            {
                try
                {
                    src = src.Replace(arrSc[i], "-");
                }
                catch { }
            }
            while (src.Contains("--"))
            {
                src = src.Replace("--", "-");
            }
            if (src.EndsWith("-"))
            {
                src = src.Substring(0, src.Length - 1);
            }
            return src;
        }
        /// <summary>
        /// Lấy chuỗi con trong 1 chuỗi
        /// </summary>
        /// <param name="source"></param>
        /// <param name="numberCharacter"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetSubString(this string source, int numberCharacter = 150, string ext = "...")
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            var leng = 0;
            if (source.Trim().Length > numberCharacter)
            {
                leng = numberCharacter;

                while (leng > 0 && source[leng].ToString() != " ")
                {
                    leng--;
                }
                return source.Substring(0, leng) + ext;
            }
            else
            {
                leng = source.Trim().Length;
                return source.Substring(0, leng);
            }
        }
        public static string SubString(this string s, int length = 150, string ext = "...")
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            if (s.Length <= length)
                return s;

            string mSource = s;
            int nLength = length;

            while (nLength > 0 && mSource[nLength].ToString() != " ")
            {
                nLength--;
            }
            mSource = mSource.Substring(0, nLength);
            return mSource + ext;
        }
        public static string RemoveHtml(this string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            return Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
        }

        public static string UnEscapeDataString(this string encodeUrlComponent)
        {
            if (string.IsNullOrWhiteSpace(encodeUrlComponent))
                return string.Empty;

            var decode = Uri.UnescapeDataString(encodeUrlComponent);

            decode = HttpUtility.HtmlDecode(decode);

            if (decode != null) decode = decode.Replace("+", " ");

            return RemoveHtml(decode);

        }
        public static string SanitizeHtml(this string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;
            html = html.Trim();
            const string acceptable = "i|b|u|sup|sub|ol|ul|li|br|h1|h2|h3|h4|h5|p|div|span|img|strong|section|table|thead|tr|td|th|a";
            const string stringPattern = @"</?(?(?=" + acceptable + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:([""']?).*?\1?)?)*\s*/?>";
            return Regex.Replace(html, stringPattern, " ");
        }

        public static string ConvertDictionaryToQuery(this Dictionary<string, object> dictionary)
        {
            string query = dictionary.Aggregate(string.Empty, (current, keyValuePair) => current + string.Format("&{0}={1}", keyValuePair.Key, keyValuePair.Value));
            query = query.TrimStart('&');

            return query;
        }

        public static string GetHash(this string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string UrlEncode(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return HttpUtility.UrlEncode(s);
            }
            return s;
        }
    }
}
