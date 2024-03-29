﻿using System;
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
    public interface IMessageBox
    {
        void ShowException(Exception exc);

        void ShowMessage(string message, string title);
    }
}
