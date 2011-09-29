
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
    // [EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'EventPresentations' query.
        [Query(IsDefault = true)]
        public IQueryable<EventPresentation> GetEventPresentations()
        {
            return this.ObjectContext.EventPresentations;
        }

        [Query]
        public IQueryable<EventPresentation> GetEventPresentationsForEvent(int id)
        {
            return this.ObjectContext.EventPresentations.Where(e=>e.EventId == id);
        }
        [Query]
        public IQueryable<EventPresentation> GetEventPresentationsCompleteForEvent(int eventId)
        {
            return this.ObjectContext.EventPresentations.Include("Presentation").Where(e => e.EventId == eventId);
        }
        public EventPresentation GetEventPresentation(int id)
        {
            return this.ObjectContext.EventPresentations.Where(e => e.Id == id).FirstOrDefault();
        }

        [Query]
        public IQueryable<Session> GetSessionFullForEventPresentation(int eventPresentationId)
        {
            var eventPresentation = this.ObjectContext.EventPresentations.Where(ep => ep.Id == eventPresentationId);
            return eventPresentation.Select(ep => ep.Sessions) as IQueryable<Session>;
        }
        [Insert]
        public void InsertEventPresentation(EventPresentation eventPresentation)
        {
            if ((eventPresentation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(eventPresentation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.EventPresentations.AddObject(eventPresentation);
            }
        }
        [Update]
        public void UpdateEventPresentation(EventPresentation currentEventPresentation)
        {
            this.ObjectContext.EventPresentations.AttachAsModified(currentEventPresentation, this.ChangeSet.GetOriginal(currentEventPresentation));
        }
        [Delete]
        public void DeleteEventPresentation(EventPresentation eventPresentation)
        {
            if ((eventPresentation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.EventPresentations.Attach(eventPresentation);
            }
            this.ObjectContext.EventPresentations.DeleteObject(eventPresentation);
        }
    }
}


