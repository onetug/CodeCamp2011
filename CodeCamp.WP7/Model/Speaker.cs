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
using System.Xml.Serialization;

namespace CodeCamp.WP7.Model
{
    public class Speaker
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string Twitter { get; set; }

        [XmlAttribute]
        public string Blog { get; set; }

        [XmlAttribute]
        public string Bio { get; set; }
    }
}