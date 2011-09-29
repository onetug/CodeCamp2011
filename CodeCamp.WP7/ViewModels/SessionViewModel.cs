using System;
using System.ComponentModel;
using System.Diagnostics;
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
using CodeCamp.WP7.AgendaServiceRef;

namespace CodeCamp.WP7.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        public Model.Session Session { get; private set; }

        public SpeakerViewModel Speaker { get; private set; }

        public string Title
        {
            get { return Session.Name; }
        }

        public string Time
        {
            get 
            {
                string start = Session.StartTime.Substring(0, 5);
                string end = Session.EndTime.Substring(0, 5);
                if (start.StartsWith("0")) start = start.Substring(1);
                if (end.StartsWith("0")) end = end.Substring(1);
                return  start + " - " + end;  
            }
        }

        public string Level
        {
            get { return Session.Level;  }
        }

        public string Summary 
        {
            get { return Time + " (level: " + Session.Level + ")"; }
        }

        public string Description
        {
            get { return Session.Description; }
        }

        public SessionViewModel(Model.Session session)
        {
            this.Session = session;

            var speaker =
                (from s in App.Event.Speakers
                 where s.Name == session.Speaker
                 select s).FirstOrDefault();

            if (speaker != null)
                Speaker = new SpeakerViewModel(speaker, false);
        }

        public void Attend()
        {
            AgendaServiceSoapClient client = new AgendaServiceSoapClient();
            client.AttendCompleted += new EventHandler<AttendCompletedEventArgs>(client_AttendCompleted);

            var request = new AttendRequest(new AttendRequestBody(App.Event.Email, App.Event.Password, Session.Id));
            client.AttendAsync(request);
        }

        void client_AttendCompleted(object sender, AttendCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                App.Event.Agenda.Clear();
                foreach (AgendaItem item in e.Result.Body.AttendResult)
                {
                    App.Event.Agenda.Add(new Model.AgendaItem()
                    {
                        SessionId = item.SessionId,
                        Rating = item.Rating,
                        Comment = item.Comment
                    });
                }

                MessageBox.Show("Attendance saved.");
            }
            else
            {
                MessageBox.Show("Saving attendance failed. Please try again later.");
            }
        }
    }
}