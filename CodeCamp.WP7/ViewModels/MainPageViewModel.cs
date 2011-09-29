using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

using CodeCamp.WP7.AgendaServiceRef;
using System.Collections.Generic;

namespace CodeCamp.WP7.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public Model.Event Event { get; private set; }

        public ObservableCollection<TrackViewModel> Tracks { get; private set; }

        public ObservableCollection<SpeakerViewModel> Speakers { get; private set; }

        public ObservableCollection<AgendaItemViewModel> Agenda { get; private set; }

        public string Email 
        {
            get { return Event.Email; }
            set
            {
                if (Event.Email != value)
                {
                    Event.Email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string Password
        {
            get { return Event.Password; }
            set
            {
                if (Event.Password != value)
                {
                    Event.Password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        public MainPageViewModel(Model.Event e)
        {
            Tracks = new ObservableCollection<TrackViewModel>();
            Speakers = new ObservableCollection<SpeakerViewModel>();
            Agenda = new ObservableCollection<AgendaItemViewModel>();

            this.Event = e;

            foreach (Model.Track track in e.Tracks)
                Tracks.Add(new TrackViewModel(track));

            foreach (Model.Speaker speaker in e.Speakers)
                Speakers.Add(new SpeakerViewModel(speaker, false));

            foreach (Model.AgendaItem item in e.Agenda)
                Agenda.Add(new AgendaItemViewModel(item));
        }

        public void Login()
        {
            AgendaServiceSoapClient client = new AgendaServiceSoapClient();
            client.GetAgendaItemsCompleted += new EventHandler<GetAgendaItemsCompletedEventArgs>(client_GetAgendaItemsCompleted);

            var request = new GetAgendaItemsRequest(new GetAgendaItemsRequestBody(Email, Password));
            client.GetAgendaItemsAsync(request);
        }

        void client_GetAgendaItemsCompleted(object sender, GetAgendaItemsCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Login failed.");
            }
            else
            {
                Event.Agenda.Clear();

                foreach (AgendaItem item in e.Result.Body.GetAgendaItemsResult)
                {
                    Event.Agenda.Add(new Model.AgendaItem() 
                    { 
                        SessionId = item.SessionId, 
                        Rating = item.Rating, 
                        Comment = item.Comment
                    });
                }

                Agenda.Clear();

                foreach (Model.AgendaItem item in Event.Agenda)
                {
                    Agenda.Add(new AgendaItemViewModel(item));
                }

                NotifyPropertyChanged("Agenda");
                NotifyPropertyChanged("ShowAgenda");

                SelectedIndex = 1;
            }
        }

        public bool ShowAgenda
        {
            get { return Agenda != null && Agenda.Count > 0; }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set 
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    NotifyPropertyChanged("SelectedIndex");
                }
            }
        }
    }
}