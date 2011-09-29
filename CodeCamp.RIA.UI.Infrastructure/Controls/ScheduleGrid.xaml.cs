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
using System.Windows.Markup;
using System.Collections.ObjectModel;
using CodeCamp.RIA.UI.Infrastructure.Model;
using System.Collections.Specialized;
using BE = CodeCamp.RIA.UI.Infrastructure.Model;

namespace CodeCamp.RIA.UI.Infrastructure
{
    [ContentProperty("Sessions")]
    public partial class ScheduleGrid : UserControl
    {
        public static readonly DependencyProperty SessionsProperty =
            DependencyProperty.Register(
                "Sessions",
                typeof(ObservableCollection<BE.Session>),
                typeof(ScheduleGrid),
                new PropertyMetadata(
                    new ObservableCollection<BE.Session>(), // default value
                    new PropertyChangedCallback(OnSessionsChanged))); // callback

        public ObservableCollection<BE.Session> Sessions
        {
            get
            {
                return (ObservableCollection<BE.Session>)GetValue(SessionsProperty);
            }
            set
            {
                SetValue(SessionsProperty, value);

//                this.Sessions.CollectionChanged += new NotifyCollectionChangedEventHandler(Sessions_CollectionChanged);
            }
        }

        private static void OnSessionsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ScheduleGrid grid = sender as ScheduleGrid;

            ObservableCollection<BE.Session> sessions = grid.Sessions;

            grid.Build(sessions);
        }

        public bool ShowTimeslots { get; set; }

        public ScheduleGrid()
        {
            ShowTimeslots = true;

            InitializeComponent();

            Sessions.CollectionChanged += new NotifyCollectionChangedEventHandler(Sessions_CollectionChanged);
        }

        void Sessions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<BE.Session> sessions = (ObservableCollection<BE.Session>)sender;

            Build(sessions);
        }

        public void Build(ObservableCollection<BE.Session> sessions)
        {
            var timeslots =
                (from s in sessions
                 select s.StartTime)
                 .OrderBy(x => x.ToUniversalTime())
                 .Distinct().ToList();

            var tracks =
                (from s in sessions
                 select s.Track).Distinct().ToList();

            LayoutRoot.Children.Clear();
            LayoutRoot.RowDefinitions.Clear();
            LayoutRoot.ColumnDefinitions.Clear();

            LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });

            // build rows
            foreach (var timeslot in timeslots)
            {
                LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

                if (ShowTimeslots)
                {
                    TextBlock title = new TextBlock();
                    title.Text = timeslot.ToShortTimeString();
                    title.FontWeight = FontWeights.Bold;
                    title.TextAlignment = TextAlignment.Left;
                    title.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    title.Foreground = new SolidColorBrush(Colors.Black);

                    LayoutRoot.Children.Add(title);
                    Grid.SetRow(title, LayoutRoot.RowDefinitions.Count - 1);
                    Grid.SetColumn(title, 0);
                }
            }

            // build columns
            foreach (var track in tracks)
            {
                LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });

                TextBlock title = new TextBlock();
                title.Text = track;
                title.FontWeight = FontWeights.Bold;
                title.TextAlignment = TextAlignment.Center;
                title.Foreground = new SolidColorBrush(Colors.Black);

                LayoutRoot.Children.Add(title);
                Grid.SetRow(title, 0);
                Grid.SetColumn(title, LayoutRoot.ColumnDefinitions.Count - 1);
            }

            // one extra row and col
            LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // add sessions
            foreach (var session in sessions)
            {
                ScheduleItem item = new ScheduleItem();
                item.DataContext = session;
                item.SelectSession += new ScheduleItem.SelectSessionDelegate(item_SelectSession);

                LayoutRoot.Children.Add(item);
                Grid.SetRow(item, timeslots.IndexOf(session.StartTime) + 1);
                Grid.SetColumn(item, tracks.IndexOf(session.Track) + 1);
            }
        }

        void item_SelectSession(object sender, Session session)
        {
            if (SelectSession != null)
                SelectSession(this, session);
        }

        public delegate void SelectSessionDelegate(object sender, Session session);

        public event SelectSessionDelegate SelectSession;
    }
}