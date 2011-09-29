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

namespace CodeCamp.RIA.UI.Infrastructure.Model
{
    public class Session
    {
        public int SessionId { get; set; }

        public int TrackId { get; set; }

        public int TimeslotId { get; set; }

        public int SpeakerId { get; set; }

        public string Title { get; set; }

        public string Speaker { get; set; }

        public string Level { get; set; }

        public string Track { get; set; }

        public DateTime StartTime {get; set;}

        public string Description { get; set; }
    }
}
