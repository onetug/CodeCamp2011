﻿#pragma checksum "X:\Git Workspace\CodeCamp2011\CodeCamp.RIA.UI\Views\SpeakerView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2F3C0F13001CF891AAA2108881C04682"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CodeCamp.RIA.UI.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CodeCamp.RIA.UI.Views {
    
    
    public partial class SpeakerView : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ScrollViewer PageScrollViewer;
        
        internal CodeCamp.RIA.UI.Controls.BusyIndicatorEx BusyIndicator;
        
        internal System.Windows.Controls.StackPanel ContentStackPanel;
        
        internal System.Windows.Controls.TextBlock HeaderText;
        
        internal System.Windows.Controls.ListBox SpeakerPresentationsList;
        
        internal System.Windows.Controls.Button Add;
        
        internal System.Windows.Controls.TextBlock DetailText;
        
        internal System.Windows.Controls.TextBlock PresentatonTitle;
        
        internal System.Windows.Controls.TextBlock Level;
        
        internal System.Windows.Controls.TextBlock Status;
        
        internal System.Windows.Controls.TextBlock Description;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CodeCamp.RIA.UI;component/Views/SpeakerView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PageScrollViewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("PageScrollViewer")));
            this.BusyIndicator = ((CodeCamp.RIA.UI.Controls.BusyIndicatorEx)(this.FindName("BusyIndicator")));
            this.ContentStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentStackPanel")));
            this.HeaderText = ((System.Windows.Controls.TextBlock)(this.FindName("HeaderText")));
            this.SpeakerPresentationsList = ((System.Windows.Controls.ListBox)(this.FindName("SpeakerPresentationsList")));
            this.Add = ((System.Windows.Controls.Button)(this.FindName("Add")));
            this.DetailText = ((System.Windows.Controls.TextBlock)(this.FindName("DetailText")));
            this.PresentatonTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PresentatonTitle")));
            this.Level = ((System.Windows.Controls.TextBlock)(this.FindName("Level")));
            this.Status = ((System.Windows.Controls.TextBlock)(this.FindName("Status")));
            this.Description = ((System.Windows.Controls.TextBlock)(this.FindName("Description")));
        }
    }
}
