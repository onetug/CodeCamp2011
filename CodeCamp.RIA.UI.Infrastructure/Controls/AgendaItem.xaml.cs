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
using System.Threading;
using CodeCamp.RIA.UI.Infrastructure.Model;
using System.Windows.Controls.Primitives;

namespace CodeCamp.RIA.UI.Infrastructure.Controls
{
    public partial class AgendaItem : UserControl
    {

        Popup popup = new Popup();

        public Popup Popup
        {
            get { return popup; }
            set { popup = value; }
        }

        Timer popupTimer;
        public Timer PopupTimer
        {
            get { return popupTimer; }
            set { popupTimer = value; }
        }

        private void TimerProc(object dataContext)
        {
            PopupTimer.Dispose();


            this.Dispatcher.BeginInvoke(delegate()
            {
                BuildPopup(this.DataContext);

                this.Popup.DataContext = dataContext;

                popup.Visibility = Visibility.Visible;
            });
        }

        public enum AgendaItemState
        {
            Selected = 0,
            Regular,
            Unavailable,
            Full
        }

        public AgendaItem()
        {
            InitializeComponent();
        }

        public delegate void SelectSessionDelegate(object sender, int SessionId);

        public event SelectSessionDelegate SelectSession;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectSession != null)
                SelectSession(sender, ((Session)DataContext).SessionId);

            Selected = true;
        }

        public bool Selected
        {
            set
            {
                AgendaButton.Background = new SolidColorBrush(value ? Colors.Blue : Colors.Gray);
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            PopupTimer = new Timer(new TimerCallback(TimerProc), this.DataContext, 1000, 0);

            System.Diagnostics.Debug.WriteLine("Mouse Enter");
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (PopupTimer != null) { PopupTimer.Dispose(); PopupTimer = null; }

            this.Popup.IsOpen = false;

            System.Diagnostics.Debug.WriteLine("Mouse Exit");
        }

        void BuildPopup(object dataContext)
        {
            // Create some content to show in the popup. Typically you would 
            // create a user control.
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(5.0);

            StackPanel panel1 = new StackPanel();
            panel1.Background = new SolidColorBrush(Colors.LightGray);

            Button button1 = new Button();
            button1.Content = "Close";
            button1.Margin = new Thickness(5.0);
            button1.Click += new RoutedEventHandler(button1_Click);
            TextBlock textblock1 = new TextBlock();
            textblock1.Text = "The popup control";
            //textblock1.DataContext = dataContext; 
            textblock1.Margin = new Thickness(5.0);
            panel1.Children.Add(textblock1);
            panel1.Children.Add(button1);
            border.Child = panel1;

            // Set the Child property of Popup to the border 
            // which contains a stackpanel, textblock and button.
            this.Popup.Child = border;

            GeneralTransform gt = this.TransformToVisual(Application.Current.RootVisual as UIElement);
            Point offset = gt.Transform(new Point(0, 0));
            double controlTop = offset.Y;
            double controlLeft = offset.X;

            // Set where the popup will show up on the screen.
            this.Popup.VerticalOffset = controlTop + 20;
            this.Popup.HorizontalOffset = controlLeft + 20;

            // Open the popup.
            this.Popup.IsOpen = true;
        }

        void button1_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup.
            //this.Popup.IsOpen = false;

            // this.Popup.RenderTransform

        }
    }
}
