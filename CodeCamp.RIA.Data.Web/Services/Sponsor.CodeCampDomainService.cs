
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
        // To support paging you will need to add ordering to the 'Sponsors' query.
        [Query(IsDefault = true)]
        public IQueryable<Sponsor> GetSponsors()
        {
            return this.ObjectContext.Sponsors;
        }
        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Sponsor> GetSponsorswithSponsorshipLevel()
        {
            return this.ObjectContext.Sponsors.Include("SponsorshipLevel").OrderBy(o => o.Id);
        }

        public Sponsor GetSponsor(int id)
        {
            return this.ObjectContext.Sponsors.Where(s => s.Id == id).FirstOrDefault();
        }
        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Sponsor> GetSponsorswithAllProperties(int eventId)
        {
            return this.ObjectContext.Sponsors.Include("SponsorshipLevel").Include("Event").OrderBy(o => o.SponsorshipLevelId).Where(e => e.EventId == eventId);
        }
        [Insert]
        public void InsertSponsor(Sponsor sponsor)
        {
            if ((sponsor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sponsor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Sponsors.AddObject(sponsor);
            }
        }
        [Update]
        public void UpdateSponsor(Sponsor currentSponsor)
        {
            this.ObjectContext.Sponsors.AttachAsModified(currentSponsor, this.ChangeSet.GetOriginal(currentSponsor));
        }
        [Delete]
        public void DeleteSponsor(Sponsor sponsor)
        {
            if ((sponsor.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Sponsors.Attach(sponsor);
            }
            this.ObjectContext.Sponsors.DeleteObject(sponsor);
        }
    }
}


