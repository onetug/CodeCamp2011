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
using System.Linq;

namespace CodeCamp.WP7.ViewModels
{
    public class TrackViewModel : ViewModelBase
    {
        private Model.Track track = null;

        public string Name { get { return track.Name; } }

        public ObservableCollection<SessionViewModel> Sessions { get; private set; }

        public ObservableCollection<SpeakerViewModel> Speakers { get; private set; }

        public string Summary { get { return Speakers.Count + " speakers, " + Sessions.Count + " sessions"; } }

        public TrackViewModel(Model.Track track)
        {
            Sessions = new ObservableCollection<SessionViewModel>();
            Speakers = new ObservableCollection<SpeakerViewModel>();

            this.track = track;

            var sessions =
                (from s in App.Event.Sessions
                 where s.Track == track.Name
                 select s).ToList();

            foreach (Model.Session session in sessions)
                Sessions.Add(new SessionViewModel(session));

            var speakerNames =
                (from s in sessions select s.Speaker).Distinct();

            foreach (string name in speakerNames)
            {
                Model.Speaker speaker = 
                    (from s in App.Event.Speakers 
                     where s.Name == name
                     select s).FirstOrDefault();

                if (speaker != null)
                    Speakers.Add(new SpeakerViewModel(speaker, false));
            }
        }
    }
}