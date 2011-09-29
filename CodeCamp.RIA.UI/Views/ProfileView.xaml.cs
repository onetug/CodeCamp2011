using System.Windows;
using CodeCamp.RIA.UI.Controls;
using CodeCamp.RIA.UI.Events;
using CodeCamp.RIA.UI.ViewModels;

namespace CodeCamp.RIA.UI.Views
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;

    /// <summary>
    /// <see cref="Page"/> class to present the user's profile information for update
    /// </summary>
    [ExportPage("/ProfileView")]
    public partial class ProfileView : Page, IHandle<DataLoadedEvent>, IHandle<MessageBoxEvent>, IHandle<ErrorWindowEvent>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ProfileView"/> class.
        /// </summary>
        public ProfileView()
        {
            this.Loaded += ProfileViewLoaded;
            InitializeComponent();

            this.Title = ApplicationStrings.ProfilePageTitle;


        }
        /// <summary>
        /// Executes after the control is loaded
        /// </summary>
        private void ProfileViewLoaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            Password1.Focus();
            this.Password1.Loaded += (o, f) => this.Password1.Focus();
            var vm = ViewModelLocator.LocateForView(this) as ProfileViewModel;
            IEventAggregator eventAggregator = vm.EventAggregator;
            eventAggregator.Subscribe(this);

        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var vm = ViewModelLocator.LocateForView(this) as ProfileViewModel;
            //vm.EventAggregator.Publish(new NavigationEvent { PageNavigatedTo = "ProfileView" });
            BusyIndicator.DataContext = vm;
        }

        public void Handle(DataLoadedEvent dataLoaded)
        {
            this.BusyIndicator.IsBusy = true;
            var vm = dataLoaded.ViewModel as ProfileViewModel;
            if (vm != null)
            {
                if (!vm.IsBusy)
                {
                    vm.BusyMessage = "Loading your Profile. Please Wait...";
                    vm.IsBusy = true;
                }
                if (vm.EventPreferences != null && vm.EventPreferences.Count > 0)
                {
                    for (int i = 0; i < vm.EventPreferences.Count; i++)
                    {
                        var grp = new PreferenceValueRadioButtonGroup
                                      {
                                          Preference = vm.EventPreferences[i],
                                          Name = "Group" + (i + 1),
                                          EventAttendee = vm.EventAttendee
                                      };
                        var control = this.FindName("RB" + (i + 1)) as StackPanel;
                        if (control != null)
                            control.Children.Add(grp);
                    }
                }
                if(vm.IsBusy)
                    vm.IsBusy = false;
                this.BusyIndicator.IsBusy = false;
            }
        }

        public void Handle(MessageBoxEvent messageBox)
        {
            if (messageBox.Message.Contains("Your profile"))
            {
                IMessageBox msgBox = new StandardMessageBox();
                msgBox.ShowMessage(messageBox.Message, messageBox.Title);
            }
        }

        public void Handle(ErrorWindowEvent errorWindowEvent)
        {
            if (errorWindowEvent.ViewModelName.Contains("ProfileViewModel"))
            {
                if (!string.IsNullOrEmpty(errorWindowEvent.Message))
                    ErrorWindow.CreateNew(errorWindowEvent.Message);
                else
                    ErrorWindow.CreateNew(errorWindowEvent.Exception);
            }
        }
    }
}