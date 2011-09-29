using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCamp.WP7.Service
{
    [Serializable]
    public class Event
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public List<Track> Tracks { get; set; }

        public List<Speaker> Speakers { get; set; }

        public List<Session> Sessions { get; set; }

        public List<AgendaItem> Agenda { get; set; }

        public Event()
        {
            Tracks = new List<Track>();
            Speakers = new List<Speaker>();
            Sessions = new List<Session>();
            Agenda = new List<AgendaItem>();
        }
    }

    [Serializable]
    public class Track
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class Speaker
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Twitter { get; set; }

        public string Blog { get; set; }

        public string Bio { get; set; }
    }

    [Serializable]
    public class Session
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Track { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Speaker { get; set; }

        public string Room { get; set; }
    }

    [Serializable]
    public class AgendaItem
    {
        public int SessionId { get; set; }

        public string Rating { get; set; }

        public string Comment { get; set; }
    }
}