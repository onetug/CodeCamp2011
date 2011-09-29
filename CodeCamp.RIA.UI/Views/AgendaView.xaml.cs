namespace CodeCamp.RIA.UI.Views
{
    using Caliburn.Micro;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using System.Windows;
    using CodeCamp.RIA.UI.Infrastructure.Model;
    using CodeCamp.RIA.UI.Events;
    using CodeCamp.RIA.UI.ViewModels;
    using System;

    /// <summary>
    /// Home page for the application.
    /// </summary>
    [ExportPage("/AgendaView")]
    public partial class AgendaView : Page, IHandle<ErrorWindowEvent>
    {
        /// <summary>
        /// Creates a new <see cref="AgendaView"/> instance.
        /// </summary>
        public AgendaView()
        {
            // commenting out for 2/27/2011 deployment:
            Loaded += AgendaViewLoaded;

            InitializeComponent();

            this.Title = ApplicationStrings.HomePageTitle;

            RightScrollViewer.LayoutUpdated += new EventHandler(RightScrollViewer_LayoutUpdated);
        }

        private void AgendaViewLoaded(object sender, RoutedEventArgs e)
        {
            // commenting out for 2/27/2011 deployment:

            // an alternative for Busy State Indication; ViewModel-based also now works
            //AgendaBusyIndicator.BusyContent = "Getting your Sessions..Please Wait";
            //AgendaBusyIndicator.IsBusy = true;

            //AgendaBusyIndicator.IsBusy = false;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var vm = ViewModelLocator.LocateForView(this) as AgendaViewModel;
            IEventAggregator eventAggregator = vm.EventAggregator;
            eventAggregator.Subscribe(this);


            this.DataContext = vm;

            //this.MyAgenda.Sessions = vm.MyAgenda;
            //this.MyAgenda.Sessions.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(MyAgenda_CollectionChanged);

            //this.Schedule.Sessions = vm.Schedule;
            //this.Schedule.Sessions.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Schedule_CollectionChanged);

            vm.MyPropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vm_MyPropertyChanged);
        }

        void vm_MyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MyAgenda")
                this.MyAgenda.Build(this.MyAgenda.Sessions);

            if (e.PropertyName == "Schedule")
                this.Schedule.Build(this.Schedule.Sessions);
        }

        void MyAgenda_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.MyAgenda.Build(this.MyAgenda.Sessions);
        }

        void Schedule_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Schedule.Build(this.Schedule.Sessions);
        }

        void RightScrollViewer_LayoutUpdated(object sender, EventArgs e)
        {
            LeftScrollViewer.ScrollToVerticalOffset(RightScrollViewer.VerticalOffset);
        }

        private void Schedule_SelectSession(object sender, Session session)
        {
            var vm = ViewModelLocator.LocateForView(this) as AgendaViewModel;

            vm.SelectSession(session);

            // this.MyAgenda.Build(this.MyAgenda.Sessions);
        }

        private void MyAgenda_SelectSession(object sender, Session session)
        {
            var vm = ViewModelLocator.LocateForView(this) as AgendaViewModel;

            vm.RemoveSessionFromMyAgenda(session);

            // this.MyAgenda.Build(this.MyAgenda.Sessions);
        }

        public void Handle(ErrorWindowEvent errorWindowEvent)
        {
            if (errorWindowEvent.ViewModelName.Contains("AgendaViewModel"))
            {
                if (!string.IsNullOrEmpty(errorWindowEvent.Message))
                    ErrorWindow.CreateNew(errorWindowEvent.Message);
                else
                    ErrorWindow.CreateNew(errorWindowEvent.Exception);
            }
        }
    }
}