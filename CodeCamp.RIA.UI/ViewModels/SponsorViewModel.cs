namespace CodeCamp.RIA.UI.ViewModels
{
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using System.Collections.ObjectModel;
 
    [ExportViewModel("SponsorView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]  
    public class SponsorViewModel : Screen, IModule
    {
        #region Fields

        private IWindowManager _windowManager;
        public IEventAggregator EventAggregator;
        private ILoggingService _loggingService;

        #endregion

        #region Properties

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyOfPropertyChange(() => IsBusy);
                }
            }
        }
        
        public ObservableCollection<Sponsor> SponsorList
        {
            get
            {
                return App.Sponsors;
            }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public SponsorViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loggingService = loggingService;
        }

        #endregion
    }
}
