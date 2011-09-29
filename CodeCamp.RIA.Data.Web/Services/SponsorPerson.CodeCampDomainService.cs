
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
        // To support paging you will need to add ordering to the 'SponsorPersons' query.
        [Query(IsDefault = true)]
        public IQueryable<SponsorPerson> GetSponsorPersons()
        {
            return this.ObjectContext.SponsorPersons;
        }

        public void InsertSponsorPerson(SponsorPerson sponsorPerson)
        {
            if ((sponsorPerson.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sponsorPerson, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SponsorPersons.AddObject(sponsorPerson);
            }
        }

        public void UpdateSponsorPerson(SponsorPerson currentSponsorPerson)
        {
            this.ObjectContext.SponsorPersons.AttachAsModified(currentSponsorPerson, this.ChangeSet.GetOriginal(currentSponsorPerson));
        }

        public void DeleteSponsorPerson(SponsorPerson sponsorPerson)
        {
            if ((sponsorPerson.EntityState == EntityState.Detached))
            {
                this.ObjectContext.SponsorPersons.Attach(sponsorPerson);
            }
            this.ObjectContext.SponsorPersons.DeleteObject(sponsorPerson);
        }
    }
}


