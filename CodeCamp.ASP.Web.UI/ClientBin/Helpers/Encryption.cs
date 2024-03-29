﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CodeCamp.ASP.Web.UI
{
    public static class Encryption
    {
        #region password hash
        public static String ComputePasswordHash(string password)
        {
            var ue = new UnicodeEncoding();
            var bytes = ue.GetBytes(password);
            var sHhash = new SHA1Managed();
            var hash = sHhash.ComputeHash(bytes);

            return ByteArrayToString(hash);
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            var sb = new StringBuilder(arrInput.Length);

            for (int i = 0; i < arrInput.Length - 1; i++)
                sb.Append(arrInput[i].ToString("X2"));

            return sb.ToString();
        }

        #endregion
    }
}