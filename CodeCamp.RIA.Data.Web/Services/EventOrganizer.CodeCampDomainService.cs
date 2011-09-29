
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
        // To support paging you will need to add ordering to the 'EventOrganizers' query.
        [Query(IsDefault = true)]
        public IQueryable<EventOrganizer> GetEventOrganizers()
        {
            return this.ObjectContext.EventOrganizers;
        }
        [Query]
        public IQueryable<EventOrganizer> GetEventOrganizersForEvent(int eventId)
        {
            return this.ObjectContext.EventOrganizers.Include("Person").Include("Event").Where(eo => eo.Event.Id == eventId);
        }
        public EventOrganizer GetEventOrganizer(int personId)
        {
            return this.ObjectContext.EventOrganizers.Where(eo => eo.Person.Id == personId).FirstOrDefault();
        }
        [Insert]
        public void InsertEventOrganizer(EventOrganizer eventOrganizer)
        {
            if ((eventOrganizer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(eventOrganizer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.EventOrganizers.AddObject(eventOrganizer);
            }
        }
        [Update]
        public void UpdateEventOrganizer(EventOrganizer currentEventOrganizer)
        {
            this.ObjectContext.EventOrganizers.AttachAsModified(currentEventOrganizer, this.ChangeSet.GetOriginal(currentEventOrganizer));
        }
        [Delete]
        public void DeleteEventOrganizer(EventOrganizer eventOrganizer)
        {
            if ((eventOrganizer.EntityState == EntityState.Detached))
            {
                this.ObjectContext.EventOrganizers.Attach(eventOrganizer);
            }
            this.ObjectContext.EventOrganizers.DeleteObject(eventOrganizer);
        }
    }
}


