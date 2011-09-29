using System.Windows;
using CodeCamp.RIA.UI.Events;
using CodeCamp.RIA.UI.ViewModels;

namespace CodeCamp.RIA.UI.Views
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Caliburn.Micro;

    [ExportPage("/SponsorView")]
    public partial class SponsorView : Page
    {
        private SponsorViewModel vm;
        private double sponsorWidth = 480;
        private double currentHorizontalOffset = 0.0;
        /// <summary>
        /// Creates a new instance of the <see cref="SponsorView"/> class.
        /// </summary>
        public SponsorView()
        {
            InitializeComponent();
            Title = ApplicationStrings.SponsorPageTitle;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            double totalWidth = sv.ScrollableWidth;
            if (sv.HorizontalOffset > 0)
            {
                double incrementPerSponsor = sponsorWidth / totalWidth;
                double start = currentHorizontalOffset / totalWidth;
                if (currentHorizontalOffset > 0)
                {
                    ESDA2.From = start;
                    ESDA2.To = start - incrementPerSponsor;
                    EasingStoryboard2.Begin();
                    currentHorizontalOffset -= sponsorWidth;
                }
                //sv.ScrollToHorizontalOffset(sv.HorizontalOffset - 480);
            }
            else
            {
                currentHorizontalOffset = totalWidth - sponsorWidth;
                sv.ScrollToHorizontalOffset(currentHorizontalOffset);
            }
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            double totalWidth = sv.ScrollableWidth;
            if (sv.ScrollableWidth > 0)
            {

                double incrementPerSponsor = sponsorWidth / totalWidth;
                double start = currentHorizontalOffset / totalWidth;
                if (currentHorizontalOffset < (totalWidth - sponsorWidth))
                {
                    ESDA1.From = start;
                    ESDA1.To = start + incrementPerSponsor;
                    EasingStoryboard1.Begin();
                    currentHorizontalOffset += sponsorWidth;
                }
                else
                {
                    currentHorizontalOffset = 0.0;
                    sv.ScrollToHorizontalOffset(currentHorizontalOffset);
                }
                //sv.ScrollToHorizontalOffset(sv.HorizontalOffset + 480);
            }
        }
    }
}
