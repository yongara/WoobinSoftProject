using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MobileClickInstagram
{
    public class InstagramEncryption
    {
        private static string key = "2gwgegrgdgehru475iwk5o69837462gs";
        public string Key
        {
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length != 32)
                {
                    if (string.IsNullOrEmpty(value) || value.Length < 32)
                    {
                        key = value.PadRight(32,'0');
                    }
                    else
                    {
                        key = value.Substring(0, 32);
                    }
                }
                else
                {
                    key = value;
                }
            }
        }
                
        public static string AES256_encrypt(string input)
        {
            return AES256_encrypt(input, key);
        }

        public static string AES256_encrypt(string input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }         

            return Convert.ToBase64String(xBuff);
        }

        public static string AES256_decrypt(string input)
        {
            return AES256_decrypt(input, key);
        }

        public static string AES256_decrypt(string input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml =Convert.FromBase64String(input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }

            return Encoding.UTF8.GetString(xBuff);
        }
    }
}
