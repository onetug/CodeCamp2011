using CodeCamp.RIA.UI.Events;
using CodeCamp.RIA.UI.ViewModels;

namespace CodeCamp.RIA.UI.Views
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;

    /// <summary>
    /// <see cref="Page"/> class to present information about the current application.
    /// </summary>
    [ExportPage("/SessionView")]
    public partial class SessionView : Page
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SessionView"/> class.
        /// </summary>
        public SessionView()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.SessionsPageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}