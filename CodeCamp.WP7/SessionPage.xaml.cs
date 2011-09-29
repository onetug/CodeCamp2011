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
    public partial class SessionPage : PhoneApplicationPage
    {
        public SessionPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string index = "";
            if (NavigationContext.QueryString.TryGetValue("index", out index))
            {
                DataContext = new SessionViewModel(App.Event.Sessions[int.Parse(index)]);
            }
        }

        private void Attend_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SessionViewModel).Attend();
        }
    }
}