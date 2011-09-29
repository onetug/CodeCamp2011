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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CodeCamp.WP7.Model
{
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
}