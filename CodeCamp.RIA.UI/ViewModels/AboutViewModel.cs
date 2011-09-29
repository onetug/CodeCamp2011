namespace CodeCamp.RIA.UI.ViewModels
{

    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.UI.Events;

    [ExportViewModel("AboutView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AboutViewModel : Screen, IModule
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
        #endregion

        #region Constructors

        [ImportingConstructor]
        public AboutViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loggingService = loggingService;
        }

        #endregion

    }
}
