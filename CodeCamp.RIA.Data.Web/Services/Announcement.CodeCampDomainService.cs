
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
    //  [EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Announcements' query.
        [Query(IsDefault = true)]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)]
        public IQueryable<Announcement> GetAnnouncements()
        {
            return this.ObjectContext.Announcements.Where(e => e.PublishDate >= DateTime.MinValue);
        }

        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)]
        public IQueryable<Announcement> GetAnnouncementsPublishedForEvent(int id)
        {
            return this.ObjectContext.Announcements.Include("Event").Include("Author").Where(e => e.EventId == id && e.PublishDate >= DateTime.MinValue && e.PublishDate < DateTime.Now);
        }

        public Announcement GetAnnouncement(int id)
        {
            return this.ObjectContext.Announcements.Where(a => a.Id == id).FirstOrDefault();
        }

        [Insert]
        public void InsertAnnouncement(Announcement announcement)
        {
            if ((announcement.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(announcement, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Announcements.AddObject(announcement);
            }
        }
        [Update]
        public void UpdateAnnouncement(Announcement currentAnnouncement)
        {
            this.ObjectContext.Announcements.AttachAsModified(currentAnnouncement, this.ChangeSet.GetOriginal(currentAnnouncement));
        }
        [Delete]
        public void DeleteAnnouncement(Announcement announcement)
        {
            if ((announcement.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Announcements.Attach(announcement);
            }
            this.ObjectContext.Announcements.DeleteObject(announcement);
        }
    }
}


