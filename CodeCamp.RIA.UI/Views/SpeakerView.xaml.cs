namespace CodeCamp.RIA.UI.Views
{
    using Events;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;

    [ExportPage("/SpeakerView")]
    public partial class SpeakerView : Page, IHandle<ErrorWindowEvent>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SpeakerView"/> class.
        /// </summary>
        public SpeakerView()
        {
            InitializeComponent();

        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void Handle(ErrorWindowEvent errorWindowEvent)
        {
            if (errorWindowEvent.ViewModelName.Contains("SpeakerViewModel"))
            {
                if (!string.IsNullOrEmpty(errorWindowEvent.Message))
                    ErrorWindow.CreateNew(errorWindowEvent.Message);
                else
                    ErrorWindow.CreateNew(errorWindowEvent.Exception);
            }
        }

    }
}
