namespace CodeCamp.RIA.UI.ViewModels
{
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using CodeCamp.RIA.Data.Web;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ServiceModel.DomainServices.Client;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.UI.Events;
    using System;
    using System.Linq;
    using BE = CodeCamp.RIA.UI.Infrastructure.Model;
    using System.ComponentModel;

    [ExportViewModel("AgendaView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AgendaViewModel : Screen, IModule
    {
        public event PropertyChangedEventHandler MyPropertyChanged;

        protected void MyNotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = MyPropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private readonly CodeCampDomainContext context = new CodeCampDomainContext();
        private IWindowManager _windowManager;
        public IEventAggregator EventAggregator;
        private ILoggingService _loggingService;
        private int personId;
        private int eventId;

        [ImportingConstructor]
        public AgendaViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loggingService = loggingService;

            personId = Int32.Parse(App.PersonId);
            eventId = Int32.Parse(App.EventId);

            LoadSessions();
        }

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

        //private Track selectedTrack;
        //public Track SelectedTrack
        //{
        //    get
        //    {
        //        return selectedTrack;
        //    }
        //    set
        //    {
        //        if (selectedTrack != value)
        //        {
        //            selectedTrack = value;
        //            IsDirty = true;
        //            NotifyOfPropertyChange(() => SelectedTrack);

        //        }
        //    }
        //}

        //private ObservableCollection<Track> tracks = new ObservableCollection<Track>();
        //public ObservableCollection<Track> Tracks
        //{
        //    get
        //    {
        //        return tracks;
        //    }
        //    set
        //    {
        //        if (tracks != value)
        //        {
        //            tracks = value;
        //            IsDirty = true;
        //            NotifyOfPropertyChange(() => Tracks);
        //        }
        //    }
        //}

        private ObservableCollection<BE.Session> myAgenda = new ObservableCollection<BE.Session>();
        public ObservableCollection<BE.Session> MyAgenda { get { return myAgenda; } }

        private ObservableCollection<BE.Session> schedule = new ObservableCollection<BE.Session>();
        public ObservableCollection<BE.Session> Schedule { get { return schedule; } }

        // TODO: should this be implemented different?
        #region Mode

        private enum Mode { New, Edit }
        private Mode mode = Mode.Edit;
        public bool IsNewMode { get { return mode == Mode.New; } }
        public bool IsEditMode { get { return mode == Mode.Edit; } }

        #endregion

        // TODO: should this be implemented different?
        #region Dirty

        private bool isDirty = true;
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                if (isDirty != value)
                {
                    isDirty = value;
                    NotifyOfPropertyChange(() => IsDirty);
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        #endregion

        private void LoadSessions()
        {
            try
            {
                LoadOperation<Session> lo =
                    context.Load(context.GetSessionsByEventQuery(eventId));
                lo.Completed += delegate
                {
                    if (lo.HasError)
                    {
                        EventAggregator.Publish(new ErrorWindowEvent { Exception = lo.Error, ViewModelName="AgendaViewModel" });
                        _loggingService.LogException(lo.Error);
                    }
                    else
                    {
                        foreach (var item in lo.Entities)
                        {
                            string speakers = string.Empty;
                            var speakerList = item.EventPresentation
                                .Presentation
                                .PresentationSpeakers
                                .ToList();

                                if (speakerList.Count() > 1)
                                {
                                    item.EventPresentation
                                        .Presentation
                                        .PresentationSpeakers
                                        .ToList()
                                        .ForEach(ps =>
                                                     {
                                                         if (ps.Person != null)
                                                         {
                                                             speakers += ps.Person.Name + Environment.NewLine;
                                                         }
                                                     });
                                }
                                if (speakerList.Count() == 1)
                                {
                                    if (speakerList.FirstOrDefault().Person != null)
                                    {
                                        speakers = speakerList.FirstOrDefault().Person.Name;
                                    }
                                }

                            Schedule.Add(new BE.Session()
                            {  
                                 SessionId = item.Id,
                                 Title = item.Name,
                                 Track = item.Track.Name,
                                 // TODO - JAS Person needs to be mapped in the RIA service call
                                 Speaker = speakers,
                                 StartTime = item.StartTime,
                                 Description = item.EventPresentation.Presentation.Description,
                                 Level = item.EventPresentation.Presentation.Level
                                 
                            });
                        }

                        NotifyOfPropertyChange(() => Schedule);

                        MyNotifyPropertyChanged("Schedule");

                        // !!!
                        LoadAgenda();
                    }
                };
            }
            catch (DomainException dex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "AgendaViewModel" });
                _loggingService.LogException(dex);
            }
            catch (Exception ex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = ex, ViewModelName = "AgendaViewModel" });
                _loggingService.LogException(ex);
            }
        }

        void LoadAgenda()
        {
            // fake agenda
            var timeslots =
                (from s in Schedule
                 select s.StartTime).Distinct().ToList();

            var agenda =
                (from startTime in timeslots
                 select new BE.Session()
                 {
                     Title = "(empty slot)",
                     Track = "My Agenda",
                     StartTime = startTime
                 }).ToList();

            foreach (BE.Session s in agenda)
                MyAgenda.Add(s);

            // MyNotifyPropertyChanged("MyAgenda");


            // load it
            try
            {
                LoadOperation<SessionAttendee> lo = context.Load(context.GetAgendaQuery(eventId, personId));
                lo.Completed += delegate
                {
                    if (lo.HasError)
                    {
                        EventAggregator.Publish(new ErrorWindowEvent { Exception = lo.Error, ViewModelName = "AgendaViewModel" });
                        _loggingService.LogException(lo.Error);
                    }
                    else
                    {
                        foreach (var item in lo.Entities)
                        {
                            var session =
                                (from s in MyAgenda
                                 where s.StartTime == item.Session.StartTime
                                 select s).First();

                            session.Title = item.Session.Name;
                            // session.Speaker = item.EventPresentation.Presentation.PresentationSpeakers[0];
                            session.SessionId = item.SessionId;
                        }

                        MyNotifyPropertyChanged("MyAgenda");
                    }
                };
            }
            catch (DomainException dex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "AgendaViewModel" });
                _loggingService.LogException(dex);
            }
            catch (Exception ex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = ex, ViewModelName = "AgendaViewModel" });
                _loggingService.LogException(ex);
            }
        }

        public bool CanSave
        {
            get
            {
                return IsValid && IsDirty;
            }
        }

        bool IsValid
        {
            get
            {
                return true;

            }
        }

        public void Save()
        {
            // general pattern for use:
            // setup new, revised data entities and required children
            // then call:
            context.SubmitChanges(ChangesSubmitted, null);
        }

        public void ChangesSubmitted(SubmitOperation op)
        {
            // general pattern for use:
            if (!op.HasError)
            {
                IsBusy = false;
                if (!op.HasError)
                {
                    // do whatcha gotta do to update your VM data and tell the user if you'd like to
                    EventAggregator.Publish(new MessageBoxEvent { Message = "Your Agenda was saved.", Title = "Agenda Saved" });
                }
                else
                {
                    var message = op.Error.Message + Environment.NewLine;
                    foreach (var entityInError in op.EntitiesInError)
                    {
                        foreach (var validationError in entityInError.ValidationErrors)
                        {
                            message += validationError.ErrorMessage + Environment.NewLine;
                        }
                    }

                    // alternate Linq approach to above block:
                    // var message += op.EntitiesInError
                    //                 .SelectMany(entityInError => entityInError.ValidationErrors)
                    //                 .Aggregate(string.Empty, (current, validationError) => current + (validationError.ErrorMessage + Environment.NewLine));

                    EventAggregator.Publish(new ErrorWindowEvent { Message = message, ViewModelName = "AgendaViewModel" });
                    var ex = new Exception(message);
                    _loggingService.LogException(ex);
                    op.MarkErrorAsHandled();
                }
            }
        }

        public void RemoveSessionFromMyAgenda(BE.Session session)
        {
            InvokeOperation assignAttendee = context.DropSessionAttendee(session.SessionId, personId);

            assignAttendee.Completed += delegate { };

            var mySession =
                (from s in MyAgenda
                 where s.StartTime == session.StartTime
                 select s).First();

            if (mySession != null)
            {
                mySession.Title = "(empty slot)";
                mySession.Speaker = string.Empty;
                mySession.SessionId = 0;
            }

            NotifyOfPropertyChange(() => MyAgenda);

            MyNotifyPropertyChanged("MyAgenda");
        }

        public void SelectSession(BE.Session session)
        {
            //bool success = true;
            
            InvokeOperation<bool> assignAttendee = context.AssignAttendeeToSession(session.SessionId, personId);

            assignAttendee.Completed += delegate
            {
                bool success = assignAttendee.Value;

                if (success)
                {
                    var mySession =
                        (from s in MyAgenda
                         where s.StartTime == session.StartTime
                         select s).First();

                    if (mySession != null)
                    {
                        mySession.Title = session.Title;
                        mySession.Speaker = session.Speaker;
                        mySession.SessionId = session.SessionId;
                    }

                    NotifyOfPropertyChange(() => MyAgenda);

                    MyNotifyPropertyChanged("MyAgenda");
                }
                else
                {
                    System.Windows.MessageBox.Show("The session you have select is already full. \r Please choose another session.", "FYI", System.Windows.MessageBoxButton.OK);

                }
            };
        }
    }
}

