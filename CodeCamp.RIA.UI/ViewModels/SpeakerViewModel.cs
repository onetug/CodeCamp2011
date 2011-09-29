namespace CodeCamp.RIA.UI.ViewModels
{
    using System;
    using System.ServiceModel.DomainServices.Client;
    using CodeCamp.RIA.Data.Web;
    using Caliburn.Micro;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.UI.Events;
    using System.Linq;

    [ExportViewModel("SpeakerView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SpeakerViewModel : Screen, IModule
    {
        #region Fields

        private readonly CodeCampDomainContext context = new CodeCampDomainContext();
        private IWindowManager _windowManager;
        public IEventAggregator EventAggregator;
        private ILoggingService _loggingService;
        private PresentationViewModel vm;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public SpeakerViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loggingService = loggingService;
            EventAggregator.Publish(this);
            GetEventPresentationsForSpeaker(Int32.Parse(App.EventId), Int32.Parse(App.PersonId));
        }


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

        private ObservableCollection<Presentation> entitySpeakerPresentations = new ObservableCollection<Presentation>();
        public ObservableCollection<Presentation> EntitySpeakerPresentations
        {
            get
            {
                return entitySpeakerPresentations;
            }
            set
            {
                if (entitySpeakerPresentations != value)
                {
                    entitySpeakerPresentations = value;
                    NotifyOfPropertyChange(() => EntitySpeakerPresentations);

                }
            }
        }

        private ObservableCollection<Model.Presentation> presentations = new ObservableCollection<Model.Presentation>();
        public ObservableCollection<Model.Presentation> SpeakerPresentations
        {
            get
            {
                return presentations;
            }
            set
            {
                if (presentations != value)
                {
                    presentations = value;
                    NotifyOfPropertyChange(() => SpeakerPresentations);

                }
            }
        }

        private Model.Presentation selectedPresentation;
        public Model.Presentation SelectedPresentation
        {
            get
            {
                return selectedPresentation;
            }
            set
            {
                if (selectedPresentation != value)
                {
                    selectedPresentation = value;
                    NotifyOfPropertyChange(() => SelectedPresentation);

                }
            }
        }

        private ObservableCollection<Person> people = new ObservableCollection<Person>();
        public ObservableCollection<Person> People
        {
            get
            {
                return people;
            }
            set
            {
                if (people != value)
                {
                    people = value;
                    NotifyOfPropertyChange(() => People);

                }
            }
        }
        #endregion

        #region Commands

        public void Add()
        {
            vm = new PresentationViewModel { PageTitle = "Add a New Presentation" };
            vm.Deactivated += AddClosed;
            _windowManager.ShowDialog(vm);
        }

        private void AddClosed(object sender, DeactivationEventArgs e)
        {
            if (vm.WasCancelled)
                return;
            if (!context.IsSubmitting)
            {
                var newPresentation = vm.Presentation;
                BusyMessage = "Adding New Presentation...Please wait.";
                IsBusy = true;
                // verify we are truly adding a new presentation
                if (newPresentation.Id == 0)
                {
                    // Map Model.Presentation to new Presentation
                    var entityPresentation = new Presentation
                                                 {
                                                     Name = newPresentation.Name,
                                                     Description = newPresentation.Description,
                                                     Level = newPresentation.Level
                                                 };
                    // default EventPresentation status is Registered
                    var eventPresentation = new EventPresentation
                                                {
                                                    EventId = App.Event.Id,
                                                    ApprovalStatus = "Registered",
                                                    PresentationId = newPresentation.Id
                                                };
                    var presentationSpeaker = new PresentationSpeaker
                                                  {
                                                      PresentationsAsSpeaker_Id = newPresentation.Id,
                                                      Speakers_Id = App.LoggedInPerson.Id
                                                  };
                    entityPresentation.EventPresentations.Add(eventPresentation);
                    entityPresentation.PresentationSpeakers.Add(presentationSpeaker);
                    context.Presentations.Add(entityPresentation);
                }
                try
                {
                    context.SubmitChanges(ChangesSubmitted, null);
                }
                catch (DomainOperationException dex)
                {
                    EventAggregator.Publish(new ErrorWindowEvent {Exception = dex, ViewModelName = "SpeakerViewModel"});
                    _loggingService.LogException(dex);
                }
            }
        }

        private void ChangesSubmitted(SubmitOperation op)
        {
            IsBusy = false;
            if (!op.HasError)
            {
                EntitySpeakerPresentations.Clear();
                SpeakerPresentations.Clear();
                SelectedPresentation = null;
                GetEventPresentationsForSpeaker(App.Event.Id, App.LoggedInPerson.Id);
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

                EventAggregator.Publish(new ErrorWindowEvent { Message = message, ViewModelName="SpeakerViewModel" });
                var ex = new Exception(message);
                _loggingService.LogException(ex);
                op.MarkErrorAsHandled();
            }
        }

        public bool CanAdd
        {
            get
            {
                // TODO: magic number date that should probably be migrated to Web.Config and then passed along as an additional InitParam
                return DateTime.Now < new DateTime(DateTime.Now.Year, 3, 2, 0, 0, 0);
            }
        }

        public void Edit(string name)
        {

            var presentation = SpeakerPresentations.GetReferenceItem(p => p.Name == name);
            if (presentation != null)
            {
                SelectedPresentation = presentation;
                vm = new PresentationViewModel(this.SelectedPresentation);
                vm.Deactivated += EditClosed;
                _windowManager.ShowDialog(vm);
            }
        }

        private void EditClosed(object sender, DeactivationEventArgs e)
        {
            if (vm.WasCancelled)
                return;
            var editedPresentation = vm.Presentation;
            BusyMessage = "Saving your Presentation...Please wait.";
            IsBusy = true;
            // Find and update existing EntitySpeakerPresentation
            var presentation = EntitySpeakerPresentations.GetReferenceItem(p => p.Id == editedPresentation.Id);
            if (presentation.Id != 0)
            {
                presentation.Name = editedPresentation.Name;
                presentation.Description = editedPresentation.Description;
                presentation.Level = editedPresentation.Level;
                if (presentation.EntityState == EntityState.Detached)
                {
                    context.Presentations.Attach(presentation);
                }
                try
                {
                    context.SubmitChanges(ChangesSubmitted, null);
                }
                catch (DomainOperationException dex)
                {
                    EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName="SpeakerViewModel" });
                    _loggingService.LogException(dex);
                }
            }
        }
        public bool CanEdit
        {
            get
            {
                return SelectedPresentation != null;
            }
        }

        public void Delete(string name)
        {
            BusyMessage = "Deleting Presentation...Please Wait";
            IsBusy = true;
            var presentation = SpeakerPresentations.GetReferenceItem(p => p.Name == name);
            if (presentation != null)
            {
                SelectedPresentation = presentation;
                // Find and delete existing EntitySpeakerPresentation
                var entityPresentation = EntitySpeakerPresentations.GetReferenceItem(p => p.Id == presentation.Id);
                if (entityPresentation != null)
                {
                    try
                    {
                        foreach (PresentationSpeaker ps in entityPresentation.PresentationSpeakers)
                        {
                            context.PresentationSpeakers.Remove(ps);
                        }
                        foreach (EventPresentation ep in entityPresentation.EventPresentations)
                        {
                            context.EventPresentations.Remove(ep);
                        }
                        context.Presentations.Remove(entityPresentation);
                        context.SubmitChanges(DeleteSubmitted, null);

                    }
                    catch (DomainOperationException dex)
                    {
                        EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "SpeakerViewModel" });
                        _loggingService.LogException(dex);
                    }
                }
            }
        }

        public bool CanDelete
        {
            get
            {
                return true;
            }
        }

        private void DeleteSubmitted(SubmitOperation op)
        {
            IsBusy = false;
            if (!op.HasError)
            {
                EntitySpeakerPresentations.Clear();
                SpeakerPresentations.Clear();
                SelectedPresentation = null;
                GetEventPresentationsForSpeaker(App.Event.Id, App.LoggedInPerson.Id);
            }
            else
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = op.Error, ViewModelName = "SpeakerViewModel" });
                _loggingService.LogException(op.Error);
            }
        }

        public void Expand(string name)
        {
            var presentation = SpeakerPresentations.GetReferenceItem(p => p.Name == name);
            if (presentation != null)
            {
                SelectedPresentation = presentation;
            }
        }

        public bool CanExpand
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Methods

        private void GetEventPresentationsForSpeaker(int eventId, int personId)
        {
            BusyMessage = "Getting your Presentations...Please wait.";
            IsBusy = true;
            LoadOperation<Person> lo = context.Load(context.GetPersonWithPresentationsQuery(personId));
            lo.Completed += delegate
            {
                foreach (var p in lo.Entities)
                {
                    foreach (var ps in p.PresentationSpeakers)
                    {
                        EntitySpeakerPresentations.Add(ps.Presentation);
                        var presentation = new Model.Presentation
                        {
                            Id = ps.Presentation.Id,
                            Name = ps.Presentation.Name,
                            Description = ps.Presentation.Description,
                            Level = ps.Presentation.Level,
                            PresentationSpeakers = new ObservableCollection<PresentationSpeaker>(),
                            EventPresentations = new ObservableCollection<EventPresentation>(),
                            Files = new ObservableCollection<File>()
                        };
                        foreach (var speaker in ps.Presentation.PresentationSpeakers)
                        {
                            presentation.PresentationSpeakers.Add(speaker);
                        }
                        foreach (var eventPresentation in ps.Presentation.EventPresentations)
                        {
                            presentation.EventPresentations.Add(eventPresentation);
                        }
                        foreach (var file in ps.Presentation.Files)
                        {
                            presentation.Files.Add(file);
                        }
                        if (presentation.EventPresentations.Count > 0)
                        {
                            presentation.ApprovalStatus = ps.Presentation.EventPresentations.Where(e => e.EventId == App.Event.Id).FirstOrDefault().ApprovalStatus;
                        }
                        SpeakerPresentations.Add(presentation);
                    }
                }
                if (SpeakerPresentations.Count > 0)
                {
                    SelectedPresentation = SpeakerPresentations.FirstOrDefault();
                }
                IsBusy = false;
            };
        }

        #endregion

    }
}
