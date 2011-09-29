using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodeCamp.RIA.UI.Infrastructure.Model;

namespace CodeCamp.RIA.UI.Infrastructure.Controls
{
    public partial class AgendaGrid : UserControl
    {
        Agenda data;
        AgendaItem[] agenda;
        AgendaItem[,] sessions;

        public AgendaGrid()
        {
            InitializeComponent();

            LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) }); // spacer

            TextBlock my = new TextBlock();
            my.Foreground = new SolidColorBrush(Colors.Black);
            my.Text = "My Agenda";
            my.FontWeight = FontWeights.Bold;
            my.TextAlignment = TextAlignment.Center;

            LayoutRoot.Children.Add(my);
            Grid.SetColumn(my, 1);
            Grid.SetRow(my, 0);
        }

        public void Setup(Agenda data)
        {
            this.data = data;

            agenda = new AgendaItem[data.Timeslots.Count];
            sessions = new AgendaItem[data.Timeslots.Count, data.Tracks.Count];

            for (int ts = 0; ts < data.Timeslots.Count; ts++)
            {
                LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });

                TextBlock time = new TextBlock();
                time.Text = data.Timeslots[ts].Title;
                time.FontWeight = FontWeights.Bold;
                time.Foreground = new SolidColorBrush(Colors.Black);
                time.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                LayoutRoot.Children.Add(time);
                Grid.SetColumn(time, 0);
                Grid.SetRow(time, ts + 1);

                agenda[ts] = new AgendaItem();
                agenda[ts].DataContext = data.MyAgenda[ts];

                LayoutRoot.Children.Add(agenda[ts]);
                Grid.SetColumn(agenda[ts], 1);
                Grid.SetRow(agenda[ts], ts + 1);
            }

            for (int tr = 0; tr < data.Tracks.Count; tr++)
            {
                LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

                TextBlock track = new TextBlock();
                track.Text = data.Tracks[tr].Title;
                track.FontWeight = FontWeights.Bold;
                track.TextAlignment = TextAlignment.Center;
                track.Foreground = new SolidColorBrush(Colors.Black);

                LayoutRoot.Children.Add(track);
                Grid.SetColumn(track, tr + 2 + 1);
                Grid.SetRow(track, 0);
            }

            for (int ts = 0; ts < data.Timeslots.Count; ts++)
                for (int tr = 0; tr < data.Tracks.Count; tr++)
                {
                    sessions[ts, tr] = new AgendaItem();
                    sessions[ts, tr].DataContext = data.GetSession(data.Timeslots[ts].TimeslotId, data.Tracks[tr].TrackId);
                    sessions[ts, tr].SelectSession += new AgendaItem.SelectSessionDelegate(SelectSession);

                    LayoutRoot.Children.Add(sessions[ts, tr]);
                    Grid.SetColumn(sessions[ts, tr], tr + 2 + 1);
                    Grid.SetRow(sessions[ts, tr], ts + 1);
                }
        }


        void SelectSession(object sender, int SessionId)
        {


            data.SelectSession(SessionId);


            Session session = data.GetSession(SessionId);

            int ts = data.FindTimeslot(session.TimeslotId);

            for (int tr = 0; tr < data.Tracks.Count; tr++)
                sessions[ts, tr].Selected = false;

            agenda[ts].DataContext = data.MyAgenda[ts];
        }
    }
}
