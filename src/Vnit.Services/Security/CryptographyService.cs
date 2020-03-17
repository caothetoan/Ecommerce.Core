using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Vnit.ApplicationCore.Entities.Security;

namespace Vnit.Services.Security
{
    /// <summary>
    /// Cryptography Service
    /// </summary>
    public class CryptographyService : ICryptographyService
    {
        private readonly IApplicationConfiguration _applicationConfiguration;

        public CryptographyService(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }
        /// <summary>
        /// Create salt with size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string CreateSalt(int size)
        {
            var random = new RNGCryptoServiceProvider();

            // Empty salt array
            var salt = new byte[size];

            // Build the random bytes
            random.GetNonZeroBytes(salt);

            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }
       /// <summary>
       /// mã hóa MD5
       /// </summary>
       /// <param name="plainText"></param>
       /// <returns></returns>
        public string GetMd5Hash(string plainText)
        {
            var md5 = MD5.Create();
            return GetMd5Hash(md5, plainText);
        }

        /// <summary>
        /// Mã hóa mật khẩu theo salt và một Thuật toán mã hóa
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <param name="passwordFormat"></param>
        /// <returns></returns>
        public string GetHashedPassword(string plainText, string salt, PasswordFormat passwordFormat)
        {
          
            //get the algorithm name
            var algorithmName = GetPasswordFormatName(passwordFormat);

            var algorithm = HashAlgorithm.Create(algorithmName);
            if (algorithm == null)
                throw new Exception(string.Format("Can't find a hash algorithm with name '{0}'", algorithmName));

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            //now merge both the byte arrays
            var plainTextWithSaltBytes =
                new byte[plainTextBytes.Length + saltBytes.Length];

            for (var i = 0; i < plainTextBytes.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainTextBytes[i];
            }
            for (var i = 0; i < saltBytes.Length; i++)
            {
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
            }
            //find the hash
            var hashBytes = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hashBytes);
        }
        /// <summary>
        /// Lấy tên thuật toán mã hóa
        /// </summary>
        /// <param name="passwordFormat"></param>
        /// <returns></returns>
        public string GetPasswordFormatName(PasswordFormat passwordFormat)
        {
            //algorithm to use
            var algorithmName = "SHA1";
            switch (passwordFormat)
            {
                case PasswordFormat.Md5Hashed:
                    algorithmName = "MD5";
                    break;
                case PasswordFormat.Sha1Hashed:
                    algorithmName = "SHA1";
                    break;
                case PasswordFormat.Sha256Hashed:
                    algorithmName = "SHA256";
                    break;
            }
            return algorithmName;
        }
        /// <summary>
        /// Tạo mật khẩu ngẫu nhiên
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetRandomPassword(int length = 15)
        {
            var random = new RNGCryptoServiceProvider();

            // Empty password array
            var password = new byte[length];

            // Build the random bytes
            random.GetBytes(password);

            // Return the string encoded password
            return Convert.ToBase64String(password);
        }
        /// <summary>
        /// Mã hóa MD5
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public string Encrypt(string plainText, string key, string salt)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;
            //let's md5 the key itself.
            var md5 = MD5.Create();
            key = GetMd5Hash(md5, key);
            salt = GetMd5Hash(md5, salt);

            string cipherText;
            var rijndael = new RijndaelManaged()
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                BlockSize = 128,
                IV = Encoding.UTF8.GetBytes(salt.ToCharArray(), 0, 16)
            };
            var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                        streamWriter.Flush();
                    }
                    cipherText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return cipherText;
        }

        public string Decrypt(string cipherText, string key, string salt)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            //let's md5 the key itself.
            var md5 = MD5.Create();
            key = GetMd5Hash(md5, key);
            salt = GetMd5Hash(md5, salt);

            string plainText;
            var cipherArray = Convert.FromBase64String(cipherText);
            var rijndael = new RijndaelManaged()
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                BlockSize = 128,
                IV = Encoding.UTF8.GetBytes(salt.ToCharArray(), 0, 16)
            };
            var decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

            using (var memoryStream = new MemoryStream(cipherArray))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        plainText = streamReader.ReadToEnd();
                    }
                }
            }
            return plainText;
        }

        public string Encrypt(string plainText)
        {
            var key = _applicationConfiguration.GetSetting("encryptionKey");
            var salt = _applicationConfiguration.GetSetting("encryptionSalt");
            return Encrypt(plainText, key, salt);
        }

        public string Decrypt(string cipherText)
        {
            var key = _applicationConfiguration.GetSetting("encryptionKey");
            var salt = _applicationConfiguration.GetSetting("encryptionSalt");
            return Decrypt(cipherText, key, salt);
        }

        /// <summary>
        /// Mã hóa mật khẩu theo salt và một Thuật toán mã hóa automation
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string GetHashedPasswordAutomation(string plainText, string salt)
        {
            // hash password
            return EncryptBase64(GetMd5Hash(plainText), salt);
        }
        /// <summary>
        /// ToBase64String
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptBase64(string originalString, string key)
        {
            try
            {
                var bytes = Encoding.ASCII.GetBytes(key);
                if (string.IsNullOrEmpty(originalString))
                    throw new ArgumentNullException("The string which needs to be encrypted can not be null.");

                var cryptoProvider = new DESCryptoServiceProvider
                {
                    Padding = PaddingMode.PKCS7,
                    Mode = CipherMode.ECB,
                    Key = bytes,
                    IV = bytes
                };
                var memoryStream = new MemoryStream();
                var cryptoStream =
                    new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(), CryptoStreamMode.Write);

                var writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            catch
            {
                throw new Exception("EncryptBase64 Exception.");
            }
        }


    }
}
