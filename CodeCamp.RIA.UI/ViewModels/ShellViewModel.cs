namespace CodeCamp.RIA.UI.ViewModels
{
    using System;
    using System.Linq;
    using System.ServiceModel.DomainServices.Client;
    using System.Windows.Controls;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Events;
    using CodeCamp.RIA.UI.Views;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using System.Collections.ObjectModel;

    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IModule>.Collection.OneActive, IShell,  IHandle<ProfileEditedEvent>
    {
        #region Fields
        private CodeCampDomainContext context = new CodeCampDomainContext();
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
                    if (!isBusy)
                        BusyMessage = "";

                }
            }
        }
        private string busyMessage;
        public string BusyMessage
        {
            get
            {
                return busyMessage;
            }
            set
            {
                if (busyMessage != value)
                {
                    busyMessage = value;
                    NotifyOfPropertyChange(() => BusyMessage);

                }
            }
        }
        private Sponsor randomSponsor;
        public Sponsor RandomSponsor
        {
            get
            {
                return randomSponsor;
            }
            set
            {
                if (randomSponsor != value)
                {
                    randomSponsor = value;
                    NotifyOfPropertyChange(() => RandomSponsor);

                }
            }
        }

        private bool profileInComplete = true;
        public bool ProfileInComplete
        {
            get
            {
                return App.ProfileInComplete;
            }
            set
            {
                if (profileInComplete != value)
                {
                    profileInComplete = value;
                    App.ProfileInComplete = value;
                    NotifyOfPropertyChange(() => ProfileInComplete);
                }
            }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loggingService = loggingService;
            EventAggregator.Subscribe(this);
            BusyMessage = "Loading...";
            IsBusy = true;
            LoadSponsors();
            if (App.LoggedInPerson != null)
                this.ProfileInComplete = App.ProfileInComplete;
        }
        #endregion

        #region Methods

        public void Handle(ProfileEditedEvent profileEdited)
        {
            this.ProfileInComplete = profileEdited.ProfileInComplete;
        }

        private void LoadSponsors()
        {
            var eventId = Int32.Parse(App.EventId);

            try
            {
                LoadOperation<Sponsor> lo = context.Load(context.GetSponsorswithAllPropertiesQuery(eventId));
                lo.Completed += delegate
                {
                    IsBusy = false;
                    if (lo.HasError)
                    {
                        ErrorWindow.CreateNew(lo.Error.Message, StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                        _loggingService.LogException(lo.Error);
                    }
                    else
                    {
                        if (App.Sponsors == null)
                        {
                            App.Sponsors = new ObservableCollection<Sponsor>();
                        }
                        foreach (var sponsor in lo.Entities)
                        {
                            App.Sponsors.Add(sponsor);
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                ErrorWindow.CreateNew(ex, StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                _loggingService.LogException(ex);
            }
        }

        #endregion
    }
}
