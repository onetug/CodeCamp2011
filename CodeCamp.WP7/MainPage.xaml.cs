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
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using CodeCamp.WP7.ViewModels;
using CodeCamp.WP7.AgendaServiceRef;
using CodeCamp.WP7.Tools;

namespace CodeCamp.WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = new MainPageViewModel(App.Event);

            if ((DataContext as MainPageViewModel).ShowAgenda)
                Carousel.SelectedIndex = 1;
        }

        private void Agenda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Agenda.SelectedIndex == -1) return;

            int index = Agenda.SelectedIndex;
            NavigationService.Navigate(new Uri("/AgendaItemPage.xaml?index=" + index, UriKind.Relative));

            Agenda.SelectedIndex = -1;
        }

        private void Speakers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Speakers.SelectedIndex == -1) return;

            int index = Speakers.SelectedIndex;
            NavigationService.Navigate(new Uri("/SpeakerPage.xaml?index=" + index, UriKind.Relative));

            Speakers.SelectedIndex = -1;
        }

        private void Tracks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tracks.SelectedIndex == -1) return;

            int index = Tracks.SelectedIndex;
            NavigationService.Navigate(new Uri("/TrackPage.xaml?index=" + index, UriKind.Relative));

            Tracks.SelectedIndex = -1;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainPageViewModel).Login();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            AgendaServiceSoapClient client = new AgendaServiceSoapClient();
            client.GetEventCompleted += new EventHandler<GetEventCompletedEventArgs>(client_GetEventCompleted);

            var request = new GetEventRequest(new GetEventRequestBody(App.Event.Email, App.Event.Password));
            client.GetEventAsync(request);
        }

        void client_GetEventCompleted(object sender, GetEventCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                AgendaServiceRef.Event v = e.Result.Body.GetEventResult;

                App.Event.Tracks.Clear();
                foreach (Track t in v.Tracks)
                    App.Event.Tracks.Add(t.ToModelTrack());

                App.Event.Speakers.Clear();
                foreach (Speaker s in v.Speakers)
                    App.Event.Speakers.Add(s.ToModelSpeaker());

                App.Event.Sessions.Clear();
                foreach (Session s in v.Sessions)
                    App.Event.Sessions.Add(s.ToModelSession());

                App.Event.Agenda.Clear();
                foreach (AgendaItem a in v.Agenda)
                    App.Event.Agenda.Add(a.ToModelAgendaItem());

                DataContext = new MainPageViewModel(App.Event);

                if ((DataContext as MainPageViewModel).ShowAgenda)
                    Carousel.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Refresh failed.");
            }
        }
    }
}