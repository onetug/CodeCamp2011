

namespace CodeCamp.RIA.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.DomainServices.Client.ApplicationServices;
    using System.Windows;
    using System.Collections.ObjectModel;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.UI.Views;

    /// <summary>
    /// Main <see cref="Application"/> class.
    /// </summary>
    public partial class App : Application
    {
        public static string ActionType { get; private set; }
        public static string PersonId { get; private set; }
        public static Person LoggedInPerson { get; set; }
        public static bool ProfileInComplete { get; set; }
        public static int PreferenceCount { get; set; }
        public static string EventId { get; set; }
        public static Event Event { get; set; }
        public static ObservableCollection<Sponsor> Sponsors { get; set; }

        ILoggingService loggingService;
        ILoggingService LoggingService
        {
            get
            {
                if (this.loggingService == null)
                {
                    return this.loggingService = new LoggingService();
                }
                return this.loggingService;
            }
            set { this.loggingService = value; }
        }

        /// <summary>
        /// Creates a new <see cref="App"/> instance.
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;
                     
            InitializeComponent();

            // Create a WebContext and add it to the ApplicationLifetimeObjects
            // collection.  This will then be available as WebContext.Current.
            //var webContext = new WebContext {Authentication = new FormsAuthentication()};
            //this.ApplicationLifetimeObjects.Add(webContext);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IDictionary<string, string> parameters = e.InitParams;

            if (parameters.Count > 0)
            {
                ActionType = parameters["Action"];
                PersonId = parameters["Person"];
                EventId = parameters["Event"];
            }
            if (!string.IsNullOrEmpty(EventId))
            {
                var context = new CodeCampDomainContext();

                var lo = context.Load(context.GetEventQuery(Int32.Parse(EventId)));
                lo.Completed += delegate
                {
                    Event = lo.Entities.SingleOrDefault();

                    if (lo.HasError)
                    {
                        this.LoggingService.LogException(lo.Error.GetBaseException());
                        ErrorWindow.CreateNew(lo.Error.Message,StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);                        
                    }
                    // TODO: Do we still need this?
                    this.LoggingService.LogMessage(string.Format("Action:{0} | Person:{1} | EventId:{2}", ActionType, PersonId, EventId));
                };
            }
        }

        /// <summary>
        /// Invoked when the <see cref="LoadUserOperation"/> completes. Use this
        /// event handler to switch from the "loading UI" you created in
        /// <see cref="InitializeRootVisual"/> to the "application UI"
        /// </summary>
        //private void Application_UserLoaded(LoadUserOperation operation)
        //{
        //}

        /// <summary>
        /// Initializes the <see cref="Application.RootVisual"/> property. The
        /// initial UI will be displayed before the LoadUser operation has completed
        /// (The LoadUser operation will cause user to be logged automatically if
        /// using windows authentication or if the user had selected the "keep
        /// me signed in" option on a previous login).
        /// </summary>
        //protected virtual void InitializeRootVisual()
        //{
        //    this.busyIndicator = new BusyIndicator
        //                             {
        //                                 Content = new ShellView(),
        //                                 HorizontalContentAlignment = HorizontalAlignment.Stretch,
        //                                 VerticalContentAlignment = VerticalAlignment.Stretch,
        //                                 BusyContent = "Loading..."
        //                             };

        //    this.RootVisual = this.busyIndicator;
        //}

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // a ChildWindow control.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                ErrorWindow.CreateNew(e.ExceptionObject, StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
            }
        }

        /// <summary>
        /// Gets the param.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>A string representing the value from the Init Param</returns>
        private static string GetParam(string p)
        {
            if (Current.Resources[p] != null)
                return Current.Resources[p].ToString();
            else
                return string.Empty;
        }


    }
}