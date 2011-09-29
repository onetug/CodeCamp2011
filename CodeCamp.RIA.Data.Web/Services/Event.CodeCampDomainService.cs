
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
        // To support paging you will need to add ordering to the 'Events' query.
        [Query(IsDefault = true)]
        public IQueryable<Event> GetEvents()
        {
            return this.ObjectContext.Events;
        }

        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public Event GetEvent(int id)
        {
            return this.ObjectContext.Events.Where(e => e.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertEvent(Event @event)
        {
            if ((@event.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(@event, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Events.AddObject(@event);
            }
        }
        [Update]
        public void UpdateEvent(Event currentEvent)
        {
            this.ObjectContext.Events.AttachAsModified(currentEvent, this.ChangeSet.GetOriginal(currentEvent));
        }
        [Delete]
        public void DeleteEvent(Event @event)
        {
            if ((@event.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Events.Attach(@event);
            }
            this.ObjectContext.Events.DeleteObject(@event);
        }
    }
}


