﻿#pragma checksum "X:\Git Workspace\CodeCamp2011\CodeCamp.RIA.UI\Views\SponsorView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "582A8EE9EBFB68601499A6B5FE5CD3FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CodeCamp.RIA.UI;
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
    
    
    public partial class SponsorView : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Media.Animation.Storyboard EasingStoryboard1;
        
        internal System.Windows.Media.Animation.DoubleAnimation ESDA1;
        
        internal System.Windows.Media.Animation.Storyboard EasingStoryboard2;
        
        internal System.Windows.Media.Animation.DoubleAnimation ESDA2;
        
        internal System.Windows.Controls.ScrollViewer PageScrollViewer;
        
        internal System.Windows.Controls.StackPanel ContentStackPanel;
        
        internal System.Windows.Controls.TextBlock HeaderText;
        
        internal System.Windows.Controls.Button ButtonLeft;
        
        internal System.Windows.Controls.ScrollViewer sv;
        
        internal System.Windows.Controls.ItemsControl SponsorList;
        
        internal System.Windows.Controls.Button ButtonRight;
        
        internal System.Windows.Controls.TextBlock ContentText;
        
        internal CodeCamp.RIA.UI.ScrollViewerOffsetMediator Mediator;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/CodeCamp.RIA.UI;component/Views/SponsorView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.EasingStoryboard1 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("EasingStoryboard1")));
            this.ESDA1 = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("ESDA1")));
            this.EasingStoryboard2 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("EasingStoryboard2")));
            this.ESDA2 = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("ESDA2")));
            this.PageScrollViewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("PageScrollViewer")));
            this.ContentStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentStackPanel")));
            this.HeaderText = ((System.Windows.Controls.TextBlock)(this.FindName("HeaderText")));
            this.ButtonLeft = ((System.Windows.Controls.Button)(this.FindName("ButtonLeft")));
            this.sv = ((System.Windows.Controls.ScrollViewer)(this.FindName("sv")));
            this.SponsorList = ((System.Windows.Controls.ItemsControl)(this.FindName("SponsorList")));
            this.ButtonRight = ((System.Windows.Controls.Button)(this.FindName("ButtonRight")));
            this.ContentText = ((System.Windows.Controls.TextBlock)(this.FindName("ContentText")));
            this.Mediator = ((CodeCamp.RIA.UI.ScrollViewerOffsetMediator)(this.FindName("Mediator")));
        }
    }
}
