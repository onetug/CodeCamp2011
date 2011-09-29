using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;
using CodeCamp.RIA.Data.Web;

namespace CodeCamp.RIA.UI.Views
{
    //[ExportPage("/SponsorWidgetView")]
    public partial class SponsorWidgetView : UserControl
    {
        public Sponsor RandomSponsor {get;set;}

        public SponsorWidgetView()
        {
            InitializeComponent();
            this.Loaded += ControlLoaded;
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            GetRandomSponsor();
        }

        private void GetRandomSponsor()
        {
            if (App.Sponsors == null)
            {
                App.Sponsors = new ObservableCollection<Sponsor>();
            }
            if (App.Sponsors.Count == 0)
            {
                this.RandomSponsor = new Sponsor
                {
                    Name = "ONETUG",
                    Url = "http://www.onetug.org",
                    Image = "/Images/CodeCamp-Small.png",
                    Description = "The Orlando .Net User Group",
                    SponsorshipLevel = new SponsorshipLevel { Name = "Organizer" }
                };
            }
            else
            {
                RandomSponsor = App.Sponsors.NextRandom();

            }
            this.RandomSponsorGrid.DataContext = RandomSponsor;
        }
    }
}
