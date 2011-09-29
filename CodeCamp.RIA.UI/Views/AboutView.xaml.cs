using CodeCamp.RIA.UI.Events;

namespace CodeCamp.RIA.UI.Views
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;
    using CodeCamp.RIA.UI.ViewModels;

    /// <summary>
    /// <see cref="Page"/> class to present information about the current application.
    /// </summary>
    [ExportPage("/AboutView")]
    public partial class AboutView : Page
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AboutView"/> class.
        /// </summary>
        public AboutView()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.AboutPageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //var vm = ViewModelLocator.LocateForView(this) as AboutViewModel;
            //vm.EventAggregator.Publish(new NavigationEvent { PageNavigatedTo = "AboutView" });
        }
    }
}