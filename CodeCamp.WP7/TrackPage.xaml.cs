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

namespace CodeCamp.WP7
{
    public partial class TrackPage : PhoneApplicationPage
    {
        public TrackPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string index = "";
            if (NavigationContext.QueryString.TryGetValue("index", out index))
            {
                DataContext = new TrackViewModel(App.Event.Tracks[int.Parse(index)]);
            }
        }

        private void Sessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sessions.SelectedIndex == -1) return;

            int index = App.Event.Sessions.IndexOf(((SessionViewModel)Sessions.SelectedItem).Session);

            NavigationService.Navigate(new Uri("/SessionPage.xaml?index=" + index, UriKind.Relative));

            Sessions.SelectedIndex = -1;
        }

        private void Speakers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Speakers.SelectedIndex == -1) return;

            int index = App.Event.Speakers.IndexOf(((SpeakerViewModel)Speakers.SelectedItem).Speaker);

            NavigationService.Navigate(new Uri("/SpeakerPage.xaml?index=" + index, UriKind.Relative));

            Speakers.SelectedIndex = -1;
        }
    }
}