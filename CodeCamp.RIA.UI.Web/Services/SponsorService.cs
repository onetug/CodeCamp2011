
namespace CodeCamp.RIA.UI.Web
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
    using CodeCamp.Model;


    // Implements application logic using the CodeCampModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class SponsorService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Sponsors' query.
        public IQueryable<Sponsor> GetSponsors()
        {
            return this.ObjectContext.Sponsors;
        }

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

        public void UpdateSponsor(Sponsor currentSponsor)
        {
            this.ObjectContext.Sponsors.AttachAsModified(currentSponsor, this.ChangeSet.GetOriginal(currentSponsor));
        }

        public void DeleteSponsor(Sponsor sponsor)
        {
            if ((sponsor.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Sponsors.Attach(sponsor);
            }
            this.ObjectContext.Sponsors.DeleteObject(sponsor);
        }

        public Sponsor GetSponsor(int id)
        {
            return this.ObjectContext.Sponsors.Where(s => s.Id == id).SingleOrDefault();
        }

        [Query(IsComposable=false)]
        public Sponsor GetRandomSponsor()
        {
            //Add some logic to pull up a random sponsor
            return this.ObjectContext.Sponsors.FirstOrDefault();
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SponsorshipLevels' query.
        public IQueryable<SponsorshipLevel> GetSponsorshipLevels()
        {
            return this.ObjectContext.SponsorshipLevels;
        }

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

        public void UpdateSponsorshipLevel(SponsorshipLevel currentSponsorshipLevel)
        {
            this.ObjectContext.SponsorshipLevels.AttachAsModified(currentSponsorshipLevel, this.ChangeSet.GetOriginal(currentSponsorshipLevel));
        }

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


