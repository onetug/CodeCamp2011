using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodeCamp.RIA.UI
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
