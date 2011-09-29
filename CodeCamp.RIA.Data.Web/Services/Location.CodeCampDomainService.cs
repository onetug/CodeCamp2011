
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
        // To support paging you will need to add ordering to the 'Locations' query.
        [Query(IsDefault = true)]
        public IQueryable<Location> GetLocations()
        {
            return this.ObjectContext.Locations;
        }

        public Location GetLocationForEvent(int eventId)
        {
            return this.ObjectContext.Locations.Where(l => l.Events.FirstOrDefault().Id == eventId).FirstOrDefault();
        }
        public Location GetLocation(int id)
        {
            return this.ObjectContext.Locations.Where(l => l.Id == id).FirstOrDefault();
        }

        [Insert]
        public void InsertLocation(Location location)
        {
            if ((location.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(location, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Locations.AddObject(location);
            }
        }

        [Update]
        public void UpdateLocation(Location currentLocation)
        {
            this.ObjectContext.Locations.AttachAsModified(currentLocation, this.ChangeSet.GetOriginal(currentLocation));
        }

        [Delete]
        public void DeleteLocation(Location location)
        {
            if ((location.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Locations.Attach(location);
            }
            this.ObjectContext.Locations.DeleteObject(location);
        }
    }
}


