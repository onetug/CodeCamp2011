namespace CodeCamp.RIA.UI.Views
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;

    [ExportPage("/ErrorPage")]
    public partial class ErrorPage : Page
    {
        public ErrorPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
