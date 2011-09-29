
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
        // To support paging you will need to add ordering to the 'SponsorshipLevels' query.
        [Query(IsDefault = true)]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 720)] // 12 hours of caching
        public IQueryable<SponsorshipLevel> GetSponsorshipLevels()
        {
            return this.ObjectContext.SponsorshipLevels;
        }

        public SponsorshipLevel GetSponsorshipLevel(int id)
        {
            return this.ObjectContext.SponsorshipLevels.Where(sl => sl.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertSponsorshipLevel(SponsorshipLevel sponsorshipLevel)
        {
            if ((sponsorshipLevel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sponsorshipLevel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SponsorshipLevels.AddObject(sponsorshipLevel);
            }
        }
        [Update]
        public void UpdateSponsorshipLevel(SponsorshipLevel currentSponsorshipLevel)
        {
            this.ObjectContext.SponsorshipLevels.AttachAsModified(currentSponsorshipLevel, this.ChangeSet.GetOriginal(currentSponsorshipLevel));
        }
        [Delete]
        public void DeleteSponsorshipLevel(SponsorshipLevel sponsorshipLevel)
        {
            if ((sponsorshipLevel.EntityState == EntityState.Detached))
            {
                this.ObjectContext.SponsorshipLevels.Attach(sponsorshipLevel);
            }
            this.ObjectContext.SponsorshipLevels.DeleteObject(sponsorshipLevel);
        }
    }
}


