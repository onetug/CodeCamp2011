
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using CodeCamp.RIA.Data.Web;


    // Implements application logic using the CodeCampModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    //[EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'EventAttendees' query.
        [Query(IsDefault = true)]
        public IQueryable<EventAttendee> GetEventAttendees()
        {
            return this.ObjectContext.EventAttendees;
        }

        [Query]
        public IQueryable<EventAttendee> GetEventAttendeesForEvent(int eventId)
        {
            return this.ObjectContext.EventAttendees.Where(e=> e.EventId == eventId);
        }

        [Query]
        public IQueryable<EventAttendee> GetEventAttendeesFullForEvent(int eventId)
        {
            return this.ObjectContext.EventAttendees.Include("Event").Include("Person").Where(e => e.EventId == eventId);
        }
        public EventAttendee GetEventAttendee(int id)
        {
            return this.ObjectContext.EventAttendees.Where(e => e.Id == id).FirstOrDefault();
        }
        public EventAttendee GetEventAttendeeByPerson(int personId)
        {
            return this.ObjectContext.EventAttendees.Where(e => e.Person.Id == personId).FirstOrDefault();
        }
        public EventAttendee GetAttendeeWithPreferencesForEvent(int personId, int eventId)
        {
            return ObjectContext.EventAttendees
                .Include("EventAttendeePreferenceValues")
                .Include("EventAttendeePreferenceValues.PreferenceValue")
                .Include("EventAttendeePreferenceValues.PreferenceValue.Preference")
                .Where(ev => ev.PersonId == personId && ev.EventId == eventId).FirstOrDefault();
        }
        [Insert]
        public void InsertEventAttendee(EventAttendee eventAttendee)
        {
            if ((eventAttendee.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(eventAttendee, EntityState.Added);
            }
            else
            {
                this.ObjectContext.EventAttendees.AddObject(eventAttendee);
            }
        }
        [Update]
        public void UpdateEventAttendee(EventAttendee currentEventAttendee)
        {
            this.ObjectContext.EventAttendees.AttachAsModified(currentEventAttendee, this.ChangeSet.GetOriginal(currentEventAttendee));
        }
        [Delete]
        public void DeleteEventAttendee(EventAttendee eventAttendee)
        {
            if ((eventAttendee.EntityState == EntityState.Detached))
            {
                this.ObjectContext.EventAttendees.Attach(eventAttendee);
            }
            this.ObjectContext.EventAttendees.DeleteObject(eventAttendee);
        }
    }
}


