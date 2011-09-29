
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
        // To support paging you will need to add ordering to the 'TrackOwners' query.
        [Query(IsDefault = true)]
        public IQueryable<TrackOwner> GetTrackOwners()
        {
            return this.ObjectContext.TrackOwners;
        }

        public void InsertTrackOwner(TrackOwner trackOwner)
        {
            if ((trackOwner.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(trackOwner, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TrackOwners.AddObject(trackOwner);
            }
        }

        public void UpdateTrackOwner(TrackOwner currentTrackOwner)
        {
            this.ObjectContext.TrackOwners.AttachAsModified(currentTrackOwner, this.ChangeSet.GetOriginal(currentTrackOwner));
        }

        public void DeleteTrackOwner(TrackOwner trackOwner)
        {
            if ((trackOwner.EntityState == EntityState.Detached))
            {
                this.ObjectContext.TrackOwners.Attach(trackOwner);
            }
            this.ObjectContext.TrackOwners.DeleteObject(trackOwner);
        }
    }
}


