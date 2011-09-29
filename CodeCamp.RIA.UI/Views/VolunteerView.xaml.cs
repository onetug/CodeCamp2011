

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
    [ExportPage("/VolunteerView")]
    public partial class VolunteerView : Page
    {
        /// <summary>
        /// Creates a new instance of the <see cref="VolunteerView"/> class.
        /// </summary>
        public VolunteerView()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.VolunteerPageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}