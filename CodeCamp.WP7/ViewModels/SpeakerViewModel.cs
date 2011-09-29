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
    public class SpeakerViewModel : ViewModelBase
    {
        public Model.Speaker Speaker { get; private set; }

        public string Name { get { return Speaker.Name; } }

        public string Title { get { return Speaker.Title;  } }

        public string Blog { get { return Speaker.Blog; } }

        public string Twitter { get { return Speaker.Twitter; } }

        public string Bio { get { return Speaker.Bio; } }

        public ObservableCollection<SessionViewModel> Sessions { get; set; }

        public SpeakerViewModel(Model.Speaker speaker, bool loadSessions)
        {
            this.Speaker = speaker;

            if (loadSessions)
            {
                Sessions = new ObservableCollection<SessionViewModel>();

                var sessions =
                    (from s in App.Event.Sessions
                     where s.Speaker == speaker.Name
                     select s).ToList();

                foreach (Model.Session session in sessions)
                    Sessions.Add(new SessionViewModel(session));
            }
        }
    }
}