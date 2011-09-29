using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Xml.Linq;
using CodeCamp.WP7.Tools;

namespace CodeCamp.WP7
{
    public partial class App : Application
    {
        // private static Model.Event _event = null;

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static Model.Event Event { get; set; }
        //{
        //    get
        //    {
        //        if (_event == null)
        //            _event = LoadEvent();

        //        return _event;
        //    }
        //}

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters
                //Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            LoadData();
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            LoadData();
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            SaveData();
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            SaveData();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        //static Model.Event LoadEvent()
        //{
        //    XDocument xml = XDocument.Load(@"Data\Event.xml", LoadOptions.None);

        //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    using (IsolatedStorageFileStream fs = new IsolatedStorageFileStream("Event.xml", FileMode.Create, isf))
        //    {
        //        xml.Save(fs);
        //        fs.Close();
        //    }

        //    return Tools.IsoStore.Load<Model.Event>("Event.xml");
        //}

        static string fileName = "event.xml";

        static void LoadData()
        {
            if (!IsoStore.FileExists(fileName))
            {
                XDocument xml = XDocument.Load(@"Data\Event.xml", LoadOptions.None);

                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                using (IsolatedStorageFileStream fs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                {
                    xml.Save(fs);
                    fs.Close();
                }
            }

            // after that read it
            App.Event = IsoStore.Load<Model.Event>(fileName);
        }

        static void SaveData()
        {
            IsoStore.Save<Model.Event>(App.Event, fileName);
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}