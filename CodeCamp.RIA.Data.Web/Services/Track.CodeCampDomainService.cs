
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
        // To support paging you will need to add ordering to the 'Tracks' query.
        [Query(IsDefault = true)]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Track> GetTracks()
        {
            return this.ObjectContext.Tracks;
        }
        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Track> GetTracksForEvent(int id)
        {
            return this.ObjectContext.Tracks.Where(t => t.EventId == id);
        }
        [Query]

        public IQueryable<Track> GetTracksForEventWithSessions(int eventId)
        {
            return this.ObjectContext.Tracks.Include("Sessions").Where(t => t.EventId == eventId);
        }
        public Track GetTrack(int id)
        {
            return this.ObjectContext.Tracks.Where(t => t.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertTrack(Track track)
        {
            if ((track.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(track, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Tracks.AddObject(track);
            }
        }
        [Update]
        public void UpdateTrack(Track currentTrack)
        {
            this.ObjectContext.Tracks.AttachAsModified(currentTrack, this.ChangeSet.GetOriginal(currentTrack));
        }
        [Delete]
        public void DeleteTrack(Track track)
        {
            if ((track.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Tracks.Attach(track);
            }
            this.ObjectContext.Tracks.DeleteObject(track);
        }
    }
}


