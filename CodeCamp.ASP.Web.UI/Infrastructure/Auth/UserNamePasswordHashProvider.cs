using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CodeCamp.ASP.Web.UI.Auth
{
    public sealed class UserNamePasswordHashProvider
    {
        [SecurityCritical]
        private const string UserNameParameterName = "userName";
        private const string PasswordParameterName = "password";
        private const string HashSalt = "4c0d1b14-a401-442e-a790-e8919356a5e6";
        private const string HashAlgorithmName = "SHA256";

        public static string GenerateUserNamePasswordHash(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(UserNameParameterName);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(PasswordParameterName);
            }

            string hashInput = userName.Trim() + password.Trim() + HashSalt;
            byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(hashInput);
            byte[] outputBytes = HashAlgorithm.Create(HashAlgorithmName).ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }
    }
}