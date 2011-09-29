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

namespace CodeCamp.RIA.UI.Infrastructure.Model
{
    public class Track
    {
        public int TrackId { get; set; }

        public string Title { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
