using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.Cryptography
{
    public static class EncryptionUtility
    {
        private const string DESKEY = "6DB18246D230DF0B36C01DB9";
        public static string Encrypt(this string cipherText)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(cipherText);
            string mdo = Convert.ToBase64String(byt);
            byte[] result;
            byte[] dataToEncrypt = System.Text.Encoding.UTF8.GetBytes(cipherText);

            MD5CryptoServiceProvider hashmd5 = new();
            byte[] keyB = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(DESKEY));
            hashmd5.Clear();

            using (var tdes = new TripleDESCryptoServiceProvider { Key = keyB, Mode = CipherMode.CBC, IV = new byte[8], Padding = PaddingMode.PKCS7 })
            {
                using (ICryptoTransform cTransform = tdes.CreateEncryptor())
                {
                    result = cTransform.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                    tdes.Clear();
                }
            }

            return Convert.ToBase64String(result, 0, result.Length);
        }
        public static string Decrypt(string cipherText)
        {
            byte[] result;
            byte[] dataToDecrypt = Convert.FromBase64String(cipherText);

            MD5CryptoServiceProvider hashmd5 = new();
            byte[] keyB = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(DESKEY));
            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider { Key = keyB, Mode = CipherMode.CBC, IV = new byte[8], Padding = PaddingMode.PKCS7 };

            using (ICryptoTransform cTransform = tdes.CreateDecryptor())
            {
                result = cTransform.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                tdes.Clear();
            }

            return UTF8Encoding.UTF8.GetString(result);
        }

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static (string, string) GenerateKeys(string apiKey, string partnerID)
        {
            string timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", System.Globalization.CultureInfo.InvariantCulture);

            string data = timeStamp + partnerID + "sid_request";

            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] key = utf8.GetBytes(apiKey);
            Byte[] message = utf8.GetBytes(data);

            HMACSHA256 hash = new HMACSHA256(key);
            var signature = hash.ComputeHash(message);

            return (Convert.ToBase64String(signature), timeStamp);
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public static int CreateRandomSalt()
        {
            Byte[] _saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
              (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }

        public static string GenerateRefereshToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
