using System;
using System.Diagnostics;
using System.ServiceModel.DomainServices.Client;
using CodeCamp.RIA.Data.Web;
using CodeCamp.RIA.UI.Views;

namespace CodeCamp.RIA.UI
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    /// <summary>
    /// <see cref="UserControl"/> class providing the main UI for the application.
    /// </summary>
    public partial class MainPage : UserControl
    {
        private readonly CodeCampDomainContext context = new CodeCampDomainContext();

        /// <summary>
        /// Creates a new <see cref="MainPage"/> instance.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            var personId = Convert.ToInt32(App.PersonId);
            if (personId > 0)
            {
                GetPerson(personId);
                
            }
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

        private void GetPerson(int id)
        {
            LoadOperation lo = context.Load(context.GetPersonQuery(id));
            lo.Completed += delegate
            {
                foreach (var pers in context.Persons)
                {
                    App.LoggedInPerson = pers;

                    Dispatcher.BeginInvoke(() => this.UserWelcome.Text += App.LoggedInPerson.Name);
                }
                if (lo.HasError)
                {
                    ErrorWindow.CreateNew(lo.Error.Message);
                }
            };
        }
    }
}