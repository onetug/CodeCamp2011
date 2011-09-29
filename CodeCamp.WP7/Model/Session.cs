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
    public class Session
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Track { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string Level { get; set; }

        [XmlAttribute]
        public string StartTime { get; set; }

        [XmlAttribute]
        public string EndTime { get; set; }

        [XmlAttribute]
        public string Speaker { get; set; }

        [XmlAttribute]
        public string Room { get; set; }
    }
}