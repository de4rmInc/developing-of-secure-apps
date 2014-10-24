using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Laba1_sql_injection.Security
{
    public static class PasswordHash
    {
        public static string ComputeHash(string password)
        {
            var hash = string.Empty;
            var passBytes=Encoding.UTF8.GetBytes(password);

            using (var md5=MD5.Create())
            {
                var hashBytes = md5.ComputeHash(passBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }

            return hash;
        }

        public static bool Equals(string hash, string clearString)
        {
            return hash.Equals(ComputeHash(clearString));
        }
    }
}