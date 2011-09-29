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
    public partial class SpeakerPage : PhoneApplicationPage
    {
        public SpeakerPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string index = "";
            if (NavigationContext.QueryString.TryGetValue("index", out index))
            {
                DataContext = new SpeakerViewModel(App.Event.Speakers[int.Parse(index)], true);
            }
        }

        private void Sessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sessions.SelectedIndex == -1) return;

            int index = App.Event.Sessions.IndexOf(((SessionViewModel)Sessions.SelectedItem).Session);

            NavigationService.Navigate(new Uri("/SessionPage.xaml?index=" + index, UriKind.Relative));

            Sessions.SelectedIndex = -1;
        }
    }
}