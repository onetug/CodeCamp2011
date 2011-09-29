namespace CodeCamp.RIA.UI.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using System;
    using System.Linq;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.Data.Web;

    /// <summary>
    /// <see cref="UserControl"/> class providing the main UI for the application.
    /// </summary>
    public partial class ShellView : UserControl
    {
        private CodeCampDomainContext context { get; set; }

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
        /// Creates a new <see cref="ShellView"/> instance.
        /// </summary>
        public ShellView()
        {
            Loaded += ShellLoaded;
            InitializeComponent();
        }

        private void ShellLoaded(object sender, RoutedEventArgs e)
        {

            context = new CodeCampDomainContext();
            GetPreferenceCount(Int32.Parse(App.EventId));

 
            switch (App.ActionType)
            {
                case "agenda":
                    ContentFrame.Navigate(new System.Uri("/AgendaView", System.UriKind.Relative));
                    break;
                case "sponsor":
                    ContentFrame.Navigate(new System.Uri("/SponsorView", System.UriKind.Relative));
                    break;
                case "profile":
                    ContentFrame.Navigate(new System.Uri("/ProfileView", System.UriKind.Relative));
                    break;
                case "present":
                    ContentFrame.Navigate(new System.Uri("/SpeakerView", System.UriKind.Relative));
                    break;
                case "volunteer":
                    ContentFrame.Navigate(new System.Uri("/VolunteerView", System.UriKind.Relative));
                    break;
                case "about":
                    ContentFrame.Navigate(new System.Uri("/AboutView", System.UriKind.Relative));
                    break;
            }
        }

        /// <summary>
        /// After the Frame navigates, ensure the <see cref="HyperlinkButton"/> representing the current page is selected
        /// </summary>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                if (child is HyperlinkButton)
                {
                    var hb = child as HyperlinkButton;
                    if (hb != null && hb.NavigateUri != null)
                    {
                        VisualStateManager.GoToState(hb,
                                                     hb.NavigateUri.ToString().Equals(e.Uri.ToString())
                                                         ? "ActiveLink"
                                                         : "InactiveLink", true);
                    }
                }
            }
        }

        /// <summary>
        /// If an error occurs during navigation, show an error window
        /// </summary>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ErrorWindow.CreateNew(e.Exception);

        }
        /// <summary>
        /// Gets the current count of <see cref="CodeCamp.RIA.Data.Web.Preference"/> for this <see cref="CodeCamp.RIA.Data.Web.Event"/> to compare with the <see cref="CodeCamp.RIA.Data.Web.EventAttendee"/> count
        /// </summary>
        /// <param name="eventId">The Id of the Event</param>
        private void GetPreferenceCount(int eventId)
        {
            var lo = context.GetPreferencesForEventCount(eventId);
            lo.Completed += delegate
            {
                if (lo.HasError)
                {
                    this.LoggingService.LogException(lo.Error.GetBaseException());
                    ErrorWindow.CreateNew(lo.Error.Message, StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                    App.PreferenceCount = 0;
                }
                else
                {
                    App.PreferenceCount = lo.Value;
                    var personId = Int32.Parse(App.PersonId);
                    if (personId > 0)
                    {
                        GetPerson(personId);
                    }
                }

            };
        }
        private void GetPerson(int id)
        {
            var lo = context.Load(context.GetPersonQuery(id));
            lo.Completed += delegate
                {
                    if (lo.HasError)
                        ErrorWindow.CreateNew(lo.Error.Message,
                                                StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                    App.LoggedInPerson = lo.Entities.SingleOrDefault();
                    Dispatcher.BeginInvoke(() =>
                                                {
                                                    this.UserWelcome.Text = "Welcome, " + App.LoggedInPerson.Name;
                                                    App.ProfileInComplete =
                                                        App.LoggedInPerson.EventAttendees.FirstOrDefault()
                                                            .EventAttendeePreferenceValues.Count <
                                                        App.PreferenceCount
                                                            ? true
                                                            : false;
                                                    this.ProfileInComplete.Visibility =
                                                        App.ProfileInComplete
                                                            ? Visibility.Visible
                                                            : Visibility.Collapsed;
                                                }
                        );
                };
        }

        //private void Button_Home_Click(object sender, RoutedEventArgs e)
        //{
        //    HtmlPage.Window.Navigate(new Uri("http://www.orlandocodecamp.com",UriKind.Absolute));

        //}

    }
}