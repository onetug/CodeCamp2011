using System.ComponentModel;

namespace CodeCamp.RIA.UI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.ServiceModel.DomainServices.Client;
    using Caliburn.Micro;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure.Services;
    using CodeCamp.RIA.UI.Views;
    using System.Collections.Generic;


    [ExportViewModel("VolunteerView")]
    public class VolunteerViewModel : Screen, IModule
    {

        #region Fields

        CodeCampDomainContext CodeCampDomainContext { get; set; }

        private IWindowManager WindowManager;
        public IEventAggregator EventAggregator;
        private ILoggingService _loggingService;

        IMessageBox MessageBox { get; set; }

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

        private ObservableCollection<SessionAttendee> volunteerSessionAttendees;
        public ObservableCollection<SessionAttendee> VolunteerSessionAttendees
        {
            get
            {
                if (volunteerSessionAttendees == null)
                {
                    volunteerSessionAttendees = new ObservableCollection<SessionAttendee>();
                }
                return volunteerSessionAttendees;

            }
            set
            {
                volunteerSessionAttendees = value;
                NotifyOfPropertyChange(() => VolunteerSessionAttendees);
            }
        }
        private ObservableCollection<Session> existingVolunteerTasks;
        public ObservableCollection<Session> ExistingVolunteerTasks
        {
            get
            {
                if (existingVolunteerTasks == null)
                {
                    existingVolunteerTasks = new ObservableCollection<Session>();
                }
                return existingVolunteerTasks;

            }
            set
            {
                existingVolunteerTasks = value;
                NotifyOfPropertyChange(() => ExistingVolunteerTasks);
            }
        }

        //public CollectionViewSource SortedAvailableVolunteerTasks;

        private ObservableCollection<Session> availableVolunteerTasks;
        public ObservableCollection<Session> AvailableVolunteerTasks
        {
            get
            {
                if (availableVolunteerTasks == null)
                {
                    availableVolunteerTasks = new ObservableCollection<Session>();
                }
                return availableVolunteerTasks;

            }
            set
            {
                availableVolunteerTasks = value;
                NotifyOfPropertyChange(() => AvailableVolunteerTasks);
            }
        }
        private Session selectedAvailabledTask;
        public Session SelectedAvailableTask
        {
            get
            {
                return selectedAvailabledTask;
            }
            set
            {
                selectedAvailabledTask = value;
                NotifyOfPropertyChange(() => SelectedAvailableTask);
            }
        }

        //public CollectionViewSource SortedAssignedVolunteerTasks;

        private ObservableCollection<Session> assignedVolunteerTasks;
        public ObservableCollection<Session> AssignedVolunteerTasks
        {
            get
            {
                if (assignedVolunteerTasks == null)
                {
                    assignedVolunteerTasks = new ObservableCollection<Session>();
                }
                return assignedVolunteerTasks;
            }
            set
            {
                assignedVolunteerTasks = value;
                NotifyOfPropertyChange(() => AssignedVolunteerTasks);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        private Session selectedAssignedTask;
        public Session SelectedAssignedTask
        {
            get
            {
                return selectedAssignedTask;
            }
            set
            {
                selectedAssignedTask = value;
                NotifyOfPropertyChange(() => SelectedAssignedTask);
            }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public VolunteerViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, ILoggingService loggingService)
        {
            this.EventAggregator = eventAggregator;
            this.WindowManager = windowManager;
            _loggingService = loggingService;
            MessageBox = new StandardMessageBox();
            BusyMessage = "Getting Volunteer information...Please wait.";
            IsBusy = true;
            this.CodeCampDomainContext = new CodeCampDomainContext();

            LoadSupportingData();

        }

        #endregion

        #region Commands

        private List<int> ExistingFoundTasks = new List<int>();

        public void Save()
        {
            BusyMessage = "Saving your Volunteer Tasks...Please wait.";
            IsBusy = true;

            var personId = Int32.Parse(App.PersonId);

            // Business Rule Notes:
            // we need to Remove the ones that are dragged out, 
            // Add the ones that are dragged in, 
            // and leave the ones alone that were already there

            // We do we need to process Capacities both coming in and going out?
            // i.e., only get AvailableTasks that don't have a count of Session Attendee == Session.MacCapacity
            // or once there is one Sessin Attendee, no longer include that task?

            // we won't need to do anything with existing saved items:
            foreach (Session task in AssignedVolunteerTasks)
            {
                foreach (Session existingTask in ExistingVolunteerTasks)
                {
                    if (task.Id == existingTask.Id)
                    {
                        ExistingFoundTasks.Add(task.Id);
                        break;
                    }
                }
            }
            // find any removed from the assigned group
            foreach (Session existingTask in ExistingVolunteerTasks)
            {
                var tId = existingTask.Id;
                var foundTask = AssignedVolunteerTasks.GetReferenceItem(t => t.Id == tId);
                if (foundTask.Id == 0)
                {

                    var attendeeToRemove =
                        VolunteerSessionAttendees.Where(s => s.EventAttendeeId == personId).Where(p => p.SessionId == tId).FirstOrDefault();
                    var sessionForUpdate = attendeeToRemove.Session;
                    sessionForUpdate.Status = "Unfulfilled";
                    CodeCampDomainContext.SessionAttendees.Remove(attendeeToRemove);
                }
            }
            // find any new tasks
            foreach (Session assignedTask in AssignedVolunteerTasks)
            {
                var tId = assignedTask.Id;
                var foundTask = ExistingVolunteerTasks.GetReferenceItem(t => t.Id == tId);
                if (foundTask.Id == 0)
                {
                    var attendeeToAdd = new SessionAttendee
                    {
                        EventAttendeeId = personId,
                        SessionId = tId,
                        Rating = "None ",
                        CheckedIn = "N",
                        Comment = "None"
                    };

                    assignedTask.Status = "Confirmed";
                    attendeeToAdd.Session = assignedTask;
                    CodeCampDomainContext.SessionAttendees.Add(attendeeToAdd);
                }
            }
            try
            {
                CodeCampDomainContext.SubmitChanges(ChangesSubmitted, null);
            }
            catch (DomainOperationException dex)
            {
                ErrorWindow.CreateNew(dex, StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                _loggingService.LogException(dex);
            }
            catch (Exception ex)
            {
                ErrorWindow.CreateNew(ex, StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                _loggingService.LogException(ex);
            }
        }

        public bool CanSave
        {
            get { return true; }
        }

        private void ChangesSubmitted(SubmitOperation op)
        {
            IsBusy = false;

            if (!op.HasError)
            {
                //((IChangeTracking)CodeCampDomainContext.EntityContainer).AcceptChanges();

                var message = (AssignedVolunteerTasks.Count >= ExistingVolunteerTasks.Count)
                                    ? "Thank you for Volunteering!"
                                    : "Your status was updated.";

                MessageBox.ShowMessage(message, "Volunteer Tasks Updated");
                ExistingVolunteerTasks.Clear();
                VolunteerSessionAttendees.Clear();
                foreach (var task in AssignedVolunteerTasks)
                {
                    ExistingVolunteerTasks.Add(task);
                    foreach (var attendee in task.SessionAttendees)
                    {
                        VolunteerSessionAttendees.Add(attendee);
                    }
                }
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
                ErrorWindow.CreateNew(message,
                            StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
                var ex = new Exception(message);
                _loggingService.LogException(ex);
                op.MarkErrorAsHandled();
            }
        }

        public void ExpandAvailable(string name)
        {
            var task = AvailableVolunteerTasks.GetReferenceItem(p => p.Name == name);
            if (task != null)
            {
                SelectedAvailableTask = task;
            }
        }

        public void ExpandAssigned(string name)
        {
            var task = AssignedVolunteerTasks.GetReferenceItem(p => p.Name == name);
            if (task != null)
            {
                SelectedAssignedTask = task;
            }
        }

        #endregion

        #region Methods

        private void LoadSupportingData()
        {
        //    BusyMessage = "Loading Volunteer Tasks...Please wait.";
        //    IsBusy = true;

        //    var personId = Int32.Parse(App.PersonId);
        //    LoadOperation<Session> findUnAssignedVolunteerTasks
        //        = CodeCampDomainContext.Load(CodeCampDomainContext
        //                               .GetSessionsQuery()
        //                               .Where(s => s.SessionType == "Volunteer")
        //                               .Where(s => s.Status == "Unfulfilled"));

        //    LoadOperation<Session> findVolunteerTasksForAttendee
        //        = CodeCampDomainContext.Load(CodeCampDomainContext
        //                               .GetVolunteerSessionsQuery()
        //                               .Where(s => s.SessionType == "Volunteer"));

        //    LoadOperation<SessionAttendee> findMyVolunteerSessionAttendees
        //        = CodeCampDomainContext.Load(CodeCampDomainContext
        //                                         .GetSessionAttendeesFullForPersonQuery(personId));

        //    findVolunteerTasksForAttendee.Completed += delegate
        //    {
        //        if (findVolunteerTasksForAttendee.HasError)
        //        {
        //            ErrorWindow.CreateNew(findVolunteerTasksForAttendee.Error.Message,
        //                StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
        //            _loggingService.LogException(findVolunteerTasksForAttendee.Error);
        //        }
        //        else
        //        {
        //            foreach (Session task in findVolunteerTasksForAttendee.Entities)
        //            {
        //                foreach (
        //                    SessionAttendee attendee in task.SessionAttendees.Where(a => a.EventAttendeeId == personId))
        //                {
        //                    this.AssignedVolunteerTasks.Add(task);
        //                    // save the existing ones for comparison during save
        //                    this.ExistingVolunteerTasks.Add(task);
        //                }
        //            }
        //        }

        //    };
        //    findMyVolunteerSessionAttendees.Completed += delegate
        //    {
        //        if (findMyVolunteerSessionAttendees.HasError)
        //        {
        //            ErrorWindow.CreateNew(findMyVolunteerSessionAttendees.Error.Message,
        //            StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
        //            _loggingService.LogException(findVolunteerTasksForAttendee.Error);
        //        }
        //        else
        //        {
        //            foreach (SessionAttendee attendee in findMyVolunteerSessionAttendees.Entities)
        //            {
        //                if (attendee.Session.SessionType == "Volunteer" && attendee.Session.Status == "Confirmed")
        //                    this.VolunteerSessionAttendees.Add(attendee);
        //            }
        //        }

        //    };
        //    findUnAssignedVolunteerTasks.Completed += delegate
        //    {
        //        IsBusy = false;
        //        if (findUnAssignedVolunteerTasks.HasError)
        //        {
        //            ErrorWindow.CreateNew(findUnAssignedVolunteerTasks.Error.Message,
        //                        StackTracePolicy.OnlyWhenDebuggingOrRunningLocally);
        //            _loggingService.LogException(findUnAssignedVolunteerTasks.Error);
        //        }
        //        else
        //        {
        //            if (findUnAssignedVolunteerTasks.Entities.Count() == 0)
        //            {
        //                MessageBox.ShowMessage(
        //                    "All Volunteer tasks are currently claimed." + Environment.NewLine +
        //                    "Thank you, but please check back just" + Environment.NewLine + "before the Event.",
        //                    "No Tasks Available");
        //            }
        //            else
        //            {
        //                foreach (Session task in findUnAssignedVolunteerTasks.Entities)
        //                {
        //                    // only add unassigned volunteer tasks
        //                    if (task.SessionAttendees.Count == 0)
        //                        this.AvailableVolunteerTasks.Add(task);
        //                }
        //            }
        //            // leave this in case we try to implement sorted lists
        //            //SortedAvailableVolunteerTasks = new CollectionViewSource();
        //            //SortedAvailableVolunteerTasks.SortDescriptions.Add(new System.ComponentModel.SortDescription("Id",
        //            //    System.ComponentModel.ListSortDirection.Ascending));
        //            //SortedAvailableVolunteerTasks.Source = this.AvailableVolunteerTasks;
        //        }

        //    };
        }

        #endregion

    }
}
