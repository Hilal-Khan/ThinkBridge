using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShopBridge.HelperUtility
{
    public class _3DesSecurity
    {

        public static string Encryption(string plainText, string Key)
        {
            TripleDES des = CreateDES(Key);
            ICryptoTransform ct = des.CreateEncryptor();
            byte[] input = Encoding.Unicode.GetBytes(plainText);
            return Convert.ToBase64String((ct.TransformFinalBlock(input, 0, input.Length)));
        }

        public static string Decryption(string CypherText, string key)
        {
            byte[] b = Convert.FromBase64String(CypherText);
            TripleDES des = CreateDES(key);
            ICryptoTransform ct = des.CreateDecryptor();
            byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
            return Encoding.Unicode.GetString(output);
        }


        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }
    }
}