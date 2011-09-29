using System;
using System.Net;
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
    //[Export (typeof(IMessageBox))]
    public class StandardMessageBox : IMessageBox
    {
        public void ShowException(Exception exc)
        {
            MessageBox.Show(ConvertExceptionToString(exc), "Error Occurred", MessageBoxButton.OK);
        }

        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
        private static string ConvertExceptionToString(Exception exc)
        {
            return exc.Message;
        }

    }
}
