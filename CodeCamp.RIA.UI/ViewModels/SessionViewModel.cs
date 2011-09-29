namespace CodeCamp.RIA.UI.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel.DomainServices.Client;
    using Caliburn.Micro;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using System.ComponentModel.Composition;
    using System.Windows;
    using CodeCamp.RIA.UI.Events;
    using CodeCamp.RIA.UI.Views;

    [ExportViewModel("SessionView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SessionViewModel : Screen, IModule
    {
        #region Fields
        private readonly CodeCampDomainContext context = new CodeCampDomainContext();
        private IWindowManager _windowManager;
        public IEventAggregator EventAggregator;
        private ILoggingService _loggingService;
        private IMessageBox MessageBox { get; set; }

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

        private EntitySet<Track> trackList;
        public EntitySet<Track> TrackList
        {
            get
            {
                return trackList;
            }
            set
            {
                if (value != trackList)
                {
                    trackList = value;
                    NotifyOfPropertyChange(() => TrackList);
                }
            }
        }
        #endregion

        #region Constructors
        [ImportingConstructor]
        public SessionViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            //EventAggregator.Publish(new NavigationEvent { PageNavigatedTo = "SessionView" });
            _windowManager = windowManager;
            _loggingService = loggingService;
            MessageBox = new StandardMessageBox();
            if (!DesignerProperties.IsInDesignTool)
            {
                LoadOperation lo = context.Load(context.GetSponsorswithAllPropertiesQuery(App.Event.Id));
                lo.Completed += delegate
                {
                    TrackList = context.Tracks;
                    if (lo.HasError)
                    {
                        ErrorWindow.CreateNew(lo.Error.Message);
                        _loggingService.LogException(lo.Error);
                    }
                };
            }
            else
            {

            }
        }

        public SessionViewModel()
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                LoadOperation lo = context.Load(context.GetSponsorswithAllPropertiesQuery(App.Event.Id));
                lo.Completed += delegate
                {
                    TrackList = context.Tracks;
                    if (lo.HasError)
                    {
                        ErrorWindow.CreateNew(lo.Error.Message);
                        _loggingService.LogException(lo.Error);
                    }
                };
            }
            else
            {

            }
        }

        #endregion


    }
}
