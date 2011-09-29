
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
        // To support paging you will need to add ordering to the 'EventAttendeePreferenceValues' query.
        [Query(IsDefault = true)]
        public IQueryable<EventAttendeePreferenceValue> GetEventAttendeePreferenceValues()
        {
            return this.ObjectContext.EventAttendeePreferenceValues;
        }

        public EventAttendeePreferenceValue GetEventAttendeePreferenceValue(int id)
        {
            return this.ObjectContext.EventAttendeePreferenceValues.Where(e => e.Id == id).FirstOrDefault();
        }

        public EventAttendeePreferenceValue GetEventAttendeePreferenceValueForPreference(int prefId)
        {
            return this.ObjectContext.EventAttendeePreferenceValues.Where(e => e.PreferenceValue.PreferenceId == prefId).FirstOrDefault();
        }
        [Insert]
        public void InsertEventAttendeePreferenceValue(EventAttendeePreferenceValue eventAttendeePreferenceValue)
        {
            if ((eventAttendeePreferenceValue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(eventAttendeePreferenceValue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.EventAttendeePreferenceValues.AddObject(eventAttendeePreferenceValue);
            }
        }
        [Update]
        public void UpdateEventAttendeePreferenceValue(EventAttendeePreferenceValue currentEventAttendeePreferenceValue)
        {
            this.ObjectContext.EventAttendeePreferenceValues.AttachAsModified(currentEventAttendeePreferenceValue, this.ChangeSet.GetOriginal(currentEventAttendeePreferenceValue));
        }
        [Delete]
        public void DeleteEventAttendeePreferenceValue(EventAttendeePreferenceValue eventAttendeePreferenceValue)
        {
            if ((eventAttendeePreferenceValue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.EventAttendeePreferenceValues.Attach(eventAttendeePreferenceValue);
            }
            this.ObjectContext.EventAttendeePreferenceValues.DeleteObject(eventAttendeePreferenceValue);
        }
    }
}


