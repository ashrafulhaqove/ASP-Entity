using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace entityPractice.helper
{
    public class dHash
    {
        internal static string make(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        internal static object verify(string input, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(input, hash);
        }

        internal static string makeSpecialCharFiler(string input)
        {
            string token = BCrypt.Net.BCrypt.HashPassword(input);
            token.Replace('/', '_');
            return token;
        }

        internal static bool verifySpecialCharFiler(string input, string hash)
        {

            hash.Replace('/', '_');

            return BCrypt.Net.BCrypt.Verify(input, hash);

        }


        internal static string GetMd5(string text)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();

        }


    }
}