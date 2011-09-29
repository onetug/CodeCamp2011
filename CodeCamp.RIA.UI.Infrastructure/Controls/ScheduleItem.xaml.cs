using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodeCamp.RIA.UI.Infrastructure.Model;

namespace CodeCamp.RIA.UI.Infrastructure
{
	public partial class ScheduleItem : UserControl
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

		public ScheduleItem()
		{
			InitializeComponent();
		}

		public delegate void SelectSessionDelegate(object sender, Session session);

		public event SelectSessionDelegate SelectSession;

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (SelectSession != null)
				SelectSession(sender, (Session)DataContext);

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

			//System.Diagnostics.Debug.WriteLine("Mouse Enter");
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);

			if (PopupTimer != null) { PopupTimer.Dispose(); PopupTimer = null; }

			this.Popup.IsOpen = false;

			//System.Diagnostics.Debug.WriteLine("Mouse Exit");
		}

		void BuildPopup(object dataContext)
		{
			// Create some content to show in the popup. Typically you would 
			// create a user control.
			Border border = new Border
								{
									BorderBrush = new SolidColorBrush(Colors.Transparent), 
									BorderThickness = new Thickness(5.0)
								};

			AgendaItemPopUp box = new AgendaItemPopUp();

			border.Child = box;

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

            this.popup.LayoutUpdated += new EventHandler(popup_LayoutUpdated);
		}

        void popup_LayoutUpdated(object sender, EventArgs e)
        {
            double h = Application.Current.Host.Content.ActualHeight;
            double w = Application.Current.Host.Content.ActualWidth;
            double aw = this.Popup.Child.RenderSize.Width;
            double ah = this.Popup.Child.RenderSize.Height;

            if (this.Popup.HorizontalOffset + aw > w)
                this.Popup.HorizontalOffset = w - aw - 20;

            if (this.Popup.VerticalOffset + ah > h)
                this.Popup.VerticalOffset = h - ah - 20;
        }
	}
}