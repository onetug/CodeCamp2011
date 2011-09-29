namespace CodeCamp.RIA.UI.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ServiceModel.DomainServices.Client;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.UI.Model;
    using CodeCamp.RIA.UI.Events;
    using Caliburn.Micro;

    [ExportViewModel("ProfileView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProfileViewModel : Screen, IModule, IHandle<IsDirtyEvent>
    {
        #region Fields

        private readonly CodeCampDomainContext context = new CodeCampDomainContext();
        private IWindowManager _windowManager;
        private ILoggingService _loggingService;
        private int eventId;
        private int personId;
        // this is public for the Navigation event in any View using this VM
        public IEventAggregator EventAggregator;

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

        private EventAttendee eventAttendee;
        public EventAttendee EventAttendee
        {
            get
            {
                return eventAttendee;
            }
            set
            {
                if (eventAttendee != value)
                {
                    eventAttendee = value;
                    NotifyOfPropertyChange(() => EventAttendee);

                }
            }
        }
        private ObservableCollection<Preference> eventPreferences = new ObservableCollection<Preference>();
        public ObservableCollection<Preference> EventPreferences
        {
            get
            {
                return eventPreferences;
            }
            set
            {
                if (eventPreferences != value)
                {
                    eventPreferences = value;
                    NotifyOfPropertyChange(() => EventPreferences);

                }
            }
        }

        private ObservableCollection<Data.Web.PreferenceValue> eventPreferenceValues = new ObservableCollection<Data.Web.PreferenceValue>();
        public ObservableCollection<Data.Web.PreferenceValue> EventPreferenceValues
        {
            get
            {
                return eventPreferenceValues;
            }
            set
            {
                if (eventPreferenceValues != value)
                {
                    eventPreferenceValues = value;
                    NotifyOfPropertyChange(() => EventPreferenceValues);

                }
            }
        }
        private Person person;
        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                if (person != value)
                {
                    person = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Person);
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        private Profile profile;
        public Profile Profile
        {
            get
            {
                return profile;
            }
            set
            {
                if (profile != value)
                {
                    profile = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Profile);
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        // TODO: should this be implemented different?
        #region Dirty

        private bool isDirty;
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
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

        #region Password

        private string password1 = string.Empty;
        public string Password1
        {
            get { return password1; }
            set
            {
                if (password1 != value)
                {
                    password1 = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Password1);
                }
            }
        }

        private string password2 = string.Empty;
        public string Password2
        {
            get { return password2; }
            set
            {
                if (password2 != value)
                {
                    password2 = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Password2);
                    NotifyOfPropertyChange(() => CanSave);
                }
            }
        }

        #endregion

        #endregion

        #region Constructors

        [ImportingConstructor]
        public ProfileViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            EventAggregator = eventAggregator;
            _windowManager = windowManager;
            _loggingService = loggingService;
            personId = Int32.Parse(App.PersonId);
            eventId = Int32.Parse(App.EventId);
            BusyMessage = "Loading your Profile. Please Wait...";
            IsBusy = true;
            EventAggregator.Subscribe(this);
            LoadPreferences();
        }

        #endregion

        #region Commands

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
                if (Profile == null) return false;
                // passwords must match
                return Password1 == Password2;
            }
        }

        public void Save()
        {
            if (!context.IsSubmitting)
            {
                SetSave(false);
                BusyMessage = "Saving your Profile. Please wait...";
                IsBusy = true;

                UpdatePersonFromProfile(this.Profile);

                // only update password if they changed it
                if (!string.IsNullOrEmpty(this.Password1)
                    && !string.IsNullOrEmpty(this.Password2)
                    && Password1 == Password2)
                    Person.PasswordHash = Encryption.ComputePasswordHash(Password1);

                if (Person.EntityState == EntityState.Detached)
                {
                    context.Persons.Attach(Person);
                }
                try
                {
                    context.SubmitChanges(ChangesSubmitted, null);
                }
                catch (DomainOperationException dex)
                {
                    EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "ProfileViewModel" });
                    _loggingService.LogException(dex);
                    SetSave(true);
                }
                catch (Exception ex)
                {
                    EventAggregator.Publish(new ErrorWindowEvent { Exception = ex, ViewModelName = "ProfileViewModel" });
                    _loggingService.LogException(ex);
                    SetSave(true);
                }
            }
        }

        private void SetSave(bool value)
        {
            IsDirty = value;
        }

        private void ChangesSubmitted(SubmitOperation op)
        {
            IsBusy = false;
            if (!op.HasError)
            {
                var message = "Your profile was updated.";
                if (Person.EventAttendees
                        .Where(e => e.EventId == eventId)
                        .FirstOrDefault()
                        .EventAttendeePreferenceValues
                        .Count == App.PreferenceCount)
                {
                    message += Environment.NewLine + "Your profile is complete." + Environment.NewLine 
                            + Environment.NewLine + "Thank you!";
                }
                else
                {
                    message += Environment.NewLine + "Your profile is still incomplete." + Environment.NewLine
                                                   + "(Please select all preferences)";
                }
                EventAggregator.Publish(new MessageBoxEvent { Message = message, Title = "Profile Updated" });
                Password1 = string.Empty;
                Password2 = string.Empty;

                this.Profile = BuildProfileFromPerson(this.Person);
                EventAggregator.Publish(new ProfileEditedEvent
                {
                    ProfileInComplete = Person.EventAttendees.Where(e => e.EventId == eventId)
                        .FirstOrDefault()
                        .EventAttendeePreferenceValues
                        .Count < App.PreferenceCount ? true : false
                });
                App.LoggedInPerson = Person;
                SetSave(false);
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
                EventAggregator.Publish(new ErrorWindowEvent { Message = message, ViewModelName = "ProfileViewModel" });
                var ex = new Exception(message);
                _loggingService.LogException(ex);
                op.MarkErrorAsHandled();
                SetSave(true);
            }

        }

        #endregion

        #region Methods

        public void Handle(IsDirtyEvent dirtyEvent)
        {
            if (this.Profile != null)
                IsDirty = this.Profile.IsDirty;
            else
                IsDirty = false;
        }

        private void LoadPreferences()
        {
            try
            {
                LoadOperation<Preference> lo =
                    context.Load(context.GetPreferencesForEventQuery(eventId));
                lo.Completed += delegate
                {
                    if (lo.HasError)
                    {
                        EventAggregator.Publish(new ErrorWindowEvent { Exception = lo.Error, ViewModelName = "ProfileViewModel" });
                        _loggingService.LogException(lo.Error);
                    }
                    else
                    {
                        EventPreferences = new ObservableCollection<Preference>();
                        foreach (var item in lo.Entities)
                        {
                            EventPreferences.Add(item);
                        }
                        LoadPerson();
                    }
                };
            }
            catch (DomainException dex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "ProfileViewModel" });
                _loggingService.LogException(dex);
            }
            catch (Exception ex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = ex, ViewModelName = "ProfileViewModel" });
                _loggingService.LogException(ex);
            }

        }
        private void LoadPerson()
        {
            try
            {
                LoadOperation<Person> lo = context.Load(context.GetPersonWithPreferencesForEventQuery(personId, eventId));
                lo.Completed += delegate
                {
                    Person = lo.Entities.SingleOrDefault();
                    if (lo.HasError)
                    {
                        EventAggregator.Publish(new ErrorWindowEvent { Exception = lo.Error, ViewModelName = "ProfileViewModel" });
                        _loggingService.LogException(lo.Error);
                    }
                    else
                    {
                        LoadEventAttendee();
                    }
                };
            }
            catch (DomainException dex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "ProfileViewModel" });
                _loggingService.LogException(dex);
            }
            catch (Exception ex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = ex, ViewModelName = "ProfileViewModel" });
                _loggingService.LogException(ex);
            }
            IsDirty = false;
        }

        private void LoadEventAttendee()
        {
            try
            {
                LoadOperation<EventAttendee> lo =
                    context.Load(context.GetAttendeeWithPreferencesForEventQuery(personId, eventId));
                lo.Completed += delegate
                {
                    EventAttendee = lo.Entities.SingleOrDefault();
                    if (lo.HasError)
                    {
                        EventAggregator.Publish(new ErrorWindowEvent { Exception = lo.Error, ViewModelName = "ProfileViewModel" });
                        _loggingService.LogException(lo.Error);
                    }
                    else
                    {
                        this.Profile = BuildProfileFromPerson(this.Person);
                        EventAggregator.Publish(new DataLoadedEvent { ViewModel = this });
                    }
                };
            }
            catch (DomainException dex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = dex, ViewModelName = "ProfileViewModel" });
                _loggingService.LogException(dex);
            }
            catch (Exception ex)
            {
                EventAggregator.Publish(new ErrorWindowEvent { Exception = ex, ViewModelName = "ProfileViewModel" });
                _loggingService.LogException(ex);
            }

            IsBusy = false;
        }

        private Profile BuildProfileFromPerson(Person profilePerson)
        {
            var editingProfile = new Profile
            {
                Id = profilePerson.Id,
                Name = profilePerson.Name,
                FirstName = profilePerson.FirstName,
                LastName = profilePerson.LastName,
                Email = profilePerson.Email,
                Title = profilePerson.Title,
                Twitter = profilePerson.Twitter,
                Website = profilePerson.Website,
                Bio = profilePerson.Bio,
                Blog = profilePerson.Blog,
                ImageUrl = profilePerson.ImageUrl,
                PasswordHash = profilePerson.PasswordHash,
                EventAggregator = this.EventAggregator,
                IsDirty = false
            };
            foreach (var attendee in profilePerson.EventAttendees)
            {
                if (attendee.EventAttendeePreferenceValues.Count > 0)
                {
                    foreach (var pref in attendee.EventAttendeePreferenceValues)
                    {
                        var prefvalue = new Model.PreferenceValue
                        {
                            Id = pref.Id,
                            ProfileId = attendee.PersonId,
                            PreferenceValueId = pref.PreferenceValues_Id,
                            PreferenceId = pref.PreferenceValue.Preference.Id,
                            EventAggregator = this.EventAggregator,
                            IsDirty = false
                        };
                        editingProfile.PreferenceValues.Add(prefvalue);
                    }
                }
            }
            IsDirty = false;
            return editingProfile;
        }

        private List<int> PreferencesSet = new List<int>();
        private List<int> PreferencesNotSet = new List<int>();

        private void UpdatePersonFromProfile(Profile updatedProfile)
        {
            Person.PasswordHash = updatedProfile.PasswordHash;
            this.Person.Title = updatedProfile.Title ?? string.Empty;
            this.Person.Twitter = updatedProfile.Twitter ?? string.Empty;
            this.Person.Website = updatedProfile.Website ?? string.Empty;
            this.Person.Bio = updatedProfile.Bio ?? string.Empty;
            this.Person.Blog = updatedProfile.Blog ?? string.Empty;
            this.Person.ImageUrl = updatedProfile.ImageUrl ?? string.Empty;
            var attendee = Person.EventAttendees.Where(e => e.EventId == eventId).FirstOrDefault();

            PreferencesSet.Clear();
            PreferencesNotSet.Clear();
            var preferenceValueCount = attendee.EventAttendeePreferenceValues.Count;
            if (preferenceValueCount > 0)
            {
                foreach (var eventAttendeePreferenceValue in attendee.EventAttendeePreferenceValues)
                {
                    PreferencesSet.Add(eventAttendeePreferenceValue.PreferenceValue.PreferenceId);
                }
            }
            foreach (var preference in EventPreferences)
            {
                var pId = preference.Id;
                var foundPreferenceId = PreferencesSet.GetValueItem<int>(p => p.Equals(pId));
                if (foundPreferenceId == 0)
                {
                    PreferencesNotSet.Add(preference.Id);
                }
            }
            foreach (var foundPreferenceId in PreferencesSet)
            {
                // find the profile entry and replace its preferencevalue
                var preferenceId = foundPreferenceId;
                foreach (var preferenceValue in attendee.EventAttendeePreferenceValues
                                                        .Where(p => p.PreferenceValue.PreferenceId == preferenceId))
                {
                    var profilePreferenceValue = Profile.PreferenceValues
                            .GetReferenceItem(i => i.PreferenceId == preferenceId);
                    if (profilePreferenceValue != null)
                    {
                        preferenceValue.PreferenceValues_Id = profilePreferenceValue.PreferenceValueId;
                    }
                    break;
                }
            }
            foreach (var notFoundPreferenceId in PreferencesNotSet)
            {
                var preferenceId = notFoundPreferenceId;
                // find any profile entry and add it to the collection
                var profilePreferenceValue = Profile.PreferenceValues
                                            .GetReferenceItem(ppv => ppv.PreferenceId == preferenceId);
                var eapv = new EventAttendeePreferenceValue
                {
                    Id = -1,
                    PreferenceValues_Id = profilePreferenceValue.PreferenceValueId,
                    EventAttendees_Id = attendee.Id
                };
                // just in case they left it unchecked
                if (profilePreferenceValue.Id < 0)
                    attendee.EventAttendeePreferenceValues.Add(eapv);
            }

        }

        public void PreferenceChecked(object dataContext)
        {
            if (dataContext != null)
            {
                var pv = dataContext as Data.Web.PreferenceValue;
                if (pv != null)
                {
                    if (pv.EventAttendeePreferenceValues.Count == 0)
                    {
                        var newPreferenceValue = new Model.PreferenceValue
                        {
                            Id = -1,
                            PreferenceId = pv.PreferenceId,
                            PreferenceValueId = pv.Id,
                            IsDirty = true,
                        };
                        var foundProfilePreferenceValue = Profile.PreferenceValues
                            .GetReferenceItem(p => p.PreferenceId == pv.PreferenceId);
                        if (foundProfilePreferenceValue.Id == 0)
                            Profile.PreferenceValues.Add(newPreferenceValue);
                        else
                        {
                            foundProfilePreferenceValue.PreferenceValueId = pv.Id;
                        }
                        this.IsDirty = true;
                        NotifyOfPropertyChange(() => CanSave);
                    }
                    else
                    {
                        foreach (var prefVal in
                            Profile.PreferenceValues.Where(prefVal => prefVal.PreferenceId == pv.PreferenceId))
                        {
                            if (prefVal.PreferenceValueId != pv.Id)
                            {
                                prefVal.PreferenceValueId = pv.Id;
                                prefVal.IsDirty = true;
                                this.IsDirty = true;
                                NotifyOfPropertyChange(() => CanSave);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
