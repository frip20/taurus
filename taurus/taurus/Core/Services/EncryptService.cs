using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace taurus.Core.Services
{
    public sealed class EncryptService
    {
        private static volatile EncryptService _encryptor;
        private static object syncRoot = new Object();
        private const string KEY = "Taurus2014DR";

        private EncryptService() { }

        public static EncryptService Instance
        {
            get
            {
                if (_encryptor == null) 
                 {
                    lock (syncRoot) 
                    {
                        if (_encryptor == null)
                            _encryptor = new EncryptService();
                    }
                 }

                return _encryptor;
            } 
       }

        public string Encrypt(string encryptText)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptText);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] resultArray = tdes.CreateEncryptor().TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        
        public string Decrypt(string decryptText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(decryptText);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] resultArray = tdes.CreateDecryptor().TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }


    }
}