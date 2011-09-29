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
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

namespace CodeCamp.RIA.UI.Infrastructure.Model
{
    public class Agenda
    {
        public ObservableCollection<Timeslot> Timeslots = new ObservableCollection<Timeslot>();

        public ObservableCollection<Track> Tracks = new ObservableCollection<Track>();

        public ObservableCollection<Session> Sessions = new ObservableCollection<Session>();

        public ObservableCollection<Session> MyAgenda = new ObservableCollection<Session>();

        public Session GetSession(int TimeslotId, int TrackId)
        {
            var session =
                from s in Sessions
                where s.TimeslotId == TimeslotId && s.TrackId == TrackId
                select s;

            return session.FirstOrDefault();
        }

        public int FindTimeslot(int TimeslotId)
        {
            for (int i = 0; i < Timeslots.Count; i++)
                if (Timeslots[i].TimeslotId == TimeslotId)
                    return i;

            throw new Exception("Timeslot not found");
        }

        public Session GetSession(int SessionId)
        {
            return
                (from s in Sessions
                 where s.SessionId == SessionId
                 select s).FirstOrDefault();
        }

        public void SelectSession(int SessionId)
        {
            Session session =
                (from s in Sessions
                 where s.SessionId == SessionId
                 select s).First();

            Session agenda =
                (from a in MyAgenda
                 where a.TimeslotId == session.TimeslotId
                 select a).First();

            int i = MyAgenda.IndexOf(agenda);
            MyAgenda[i] = session;

            // TODO: unselect old

            // TODO: select new
        }

        public Agenda()
        {
            FakeData();
        }

        void FakeData()
        {
            // timeslots
            Timeslots.Add(new Timeslot() { TimeslotId = 1, Title = "7:00 AM - 8:00 AM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 2, Title = "8:00 AM - 9:00 AM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 3, Title = "9:00 AM - 10:00 AM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 4, Title = "10:00 AM - 11:00 AM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 5, Title = "11:00 AM - 12:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 6, Title = "12:00 PM - 1:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 7, Title = "1:00 PM - 2:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 8, Title = "2:00 PM - 3:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 9, Title = "3:00 PM - 4:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 10, Title = "4:00 PM - 5:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 11, Title = "5:00 PM - 6:00 PM" });
            Timeslots.Add(new Timeslot() { TimeslotId = 12, Title = "6:00 PM - 7:00 PM" });

            // tracks
            Tracks.Add(new Track() { TrackId = 1, Title = "Track 1" });
            Tracks.Add(new Track() { TrackId = 2, Title = "Track 2" });
            Tracks.Add(new Track() { TrackId = 3, Title = "Track 3" });
            Tracks.Add(new Track() { TrackId = 4, Title = "Track 4" });
            Tracks.Add(new Track() { TrackId = 5, Title = "Track 5" });
            Tracks.Add(new Track() { TrackId = 6, Title = "Track 6" });
            Tracks.Add(new Track() { TrackId = 7, Title = "Track 7" });
            Tracks.Add(new Track() { TrackId = 8, Title = "Track 8" });
            Tracks.Add(new Track() { TrackId = 9, Title = "Track 9" });
            Tracks.Add(new Track() { TrackId = 10, Title = "Track 10" });

            // agenda (prefil blank sessions)
            for (int i = 0; i < Timeslots.Count; i++)
                MyAgenda.Add(new Session() { Title = "(empty slot)", TimeslotId = Timeslots[i].TimeslotId });

            // sessions
            int id = 0;
            for (int tr = 0; tr < Tracks.Count; tr++)
                for (int ts = 0; ts < Timeslots.Count; ts++)
                {
                    Session session = new Session();
                    session.SessionId = id++;
                    session.TrackId = Tracks[tr].TrackId;
                    session.TimeslotId = Timeslots[ts].TimeslotId;
                    session.Title = "Session " + (tr + 1).ToString() + " - " + (ts + 1).ToString();
                    session.Speaker = "James Jones";
                    session.Level = "100";

                    Sessions.Add(session);
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
