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
using System.Linq;

using CodeCamp.WP7.Model;
using CodeCamp.WP7.AgendaServiceRef;

namespace CodeCamp.WP7.ViewModels
{
    public class AgendaItemViewModel : ViewModelBase
    {
        private Model.AgendaItem item { get; set; }

        private Model.Session session { get; set; }

        public SessionViewModel Session { get; private set; }

        public SpeakerViewModel Speaker { get; private set; }

        public AgendaItemViewModel(Model.AgendaItem item)
        {
            this.item = item;

            this.session =
                (from s in App.Event.Sessions
                 where s.Id == item.SessionId
                 select s).FirstOrDefault();

            if (session != null)
            {
                Session = new SessionViewModel(session);

                var speaker =
                    (from s in App.Event.Speakers
                     where s.Name == session.Speaker
                     select s).FirstOrDefault();

                if (speaker != null)
                    Speaker = new SpeakerViewModel(speaker, false);
            }
        }

        public string Title
        {
            get { return session.Name; }
        }

        public string Summary
        {
            get
            {
                string start = session.StartTime.Substring(0, 5);
                string end = session.EndTime.Substring(0, 5);
                if (start.StartsWith("0")) start = start.Substring(1);
                if (end.StartsWith("0")) end = end.Substring(1);
                string room = session.Room != "" ? ", room " + session.Room : "";

                return start + " - " + end + room;
            }
        }

        public string Comment
        {
            get { return item.Comment; }
            set
            {
                if (item.Comment != value)
                {
                    item.Comment = value;
                    NotifyPropertyChanged("Comment");
                }
            }
        }

        public string Rating 
        {
            get { return item.Rating; }
            set
            {
                if (item.Rating != value)
                {
                    item.Rating = value;

                    NotifyPropertyChanged("Rating");
                    NotifyPropertyChanged("Rating1");
                    NotifyPropertyChanged("Rating2");
                    NotifyPropertyChanged("Rating3");
                    NotifyPropertyChanged("Rating4");
                    NotifyPropertyChanged("Rating5");
                }
            }
        }

        public bool Rating1
        {
            get { return item.Rating == "1"; }
            set
            {
                if (value)
                {
                    item.Rating = "1";

                    NotifyPropertyChanged("Rating");
                    NotifyPropertyChanged("Rating1");
                    NotifyPropertyChanged("Rating2");
                    NotifyPropertyChanged("Rating3");
                    NotifyPropertyChanged("Rating4");
                    NotifyPropertyChanged("Rating5");
                }
            }
        }

        public bool Rating2
        {
            get { return item.Rating == "2"; }
            set
            {
                if (value)
                {
                    item.Rating = "2";

                    NotifyPropertyChanged("Rating");
                    NotifyPropertyChanged("Rating1");
                    NotifyPropertyChanged("Rating2");
                    NotifyPropertyChanged("Rating3");
                    NotifyPropertyChanged("Rating4");
                    NotifyPropertyChanged("Rating5");
                }
            }
        }

        public bool Rating3
        {
            get { return item.Rating == "3"; }
            set
            {
                if (value)
                {
                    item.Rating = "3";

                    NotifyPropertyChanged("Rating");
                    NotifyPropertyChanged("Rating1");
                    NotifyPropertyChanged("Rating2");
                    NotifyPropertyChanged("Rating3");
                    NotifyPropertyChanged("Rating4");
                    NotifyPropertyChanged("Rating5");
                }
            }
        }

        public bool Rating4
        {
            get { return item.Rating == "4"; }
            set
            {
                if (value)
                {
                    item.Rating = "4";

                    NotifyPropertyChanged("Rating");
                    NotifyPropertyChanged("Rating1");
                    NotifyPropertyChanged("Rating2");
                    NotifyPropertyChanged("Rating3");
                    NotifyPropertyChanged("Rating4");
                    NotifyPropertyChanged("Rating5");
                }
            }
        }

        public bool Rating5
        {
            get { return item.Rating == "5"; }
            set
            {
                if (value)
                {
                    item.Rating = "5";

                    NotifyPropertyChanged("Rating");
                    NotifyPropertyChanged("Rating1");
                    NotifyPropertyChanged("Rating2");
                    NotifyPropertyChanged("Rating3");
                    NotifyPropertyChanged("Rating4");
                    NotifyPropertyChanged("Rating5");
                }
            }
        }

        public void Submit()
        {
            AgendaServiceSoapClient client = new AgendaServiceSoapClient();
            client.UpdateRatingCompleted += new EventHandler<UpdateRatingCompletedEventArgs>(client_UpdateRatingCompleted);

            var ai = new CodeCamp.WP7.AgendaServiceRef.AgendaItem();
            ai.Comment = this.Comment;
            ai.Rating = this.Rating;
            ai.SessionId = this.item.SessionId;

            var request =
                new UpdateRatingRequest(
                    new UpdateRatingRequestBody(App.Event.Email, App.Event.Password, ai));
            
            client.UpdateRatingAsync(request);
        }

        void client_UpdateRatingCompleted(object sender, UpdateRatingCompletedEventArgs e)
        {
            if (e.Error == null)
                MessageBox.Show("Feedback saved.");
            else
                MessageBox.Show("Saving feedback failed. Please try again later.");
        }
    }
}