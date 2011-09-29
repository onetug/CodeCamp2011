
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
        // To support paging you will need to add ordering to the 'PresentationSpeakers' query.
        [Query(IsDefault = true)]
        public IQueryable<PresentationSpeaker> GetPresentationSpeakers()
        {
            return this.ObjectContext.PresentationSpeakers;
        }
        [Query]
        public IQueryable<PresentationSpeaker> GetPresentationSpeakersFull()
        {
            return this.ObjectContext.PresentationSpeakers.Include("Presentation").Include("Person");
        }
        [Insert]
        public void InsertPresentationSpeaker(PresentationSpeaker presentationSpeaker)
        {
            if ((presentationSpeaker.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(presentationSpeaker, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PresentationSpeakers.AddObject(presentationSpeaker);
            }
        }
        [Update]
        public void UpdatePresentationSpeaker(PresentationSpeaker currentPresentationSpeaker)
        {
            this.ObjectContext.PresentationSpeakers.AttachAsModified(currentPresentationSpeaker, this.ChangeSet.GetOriginal(currentPresentationSpeaker));
        }
        [Delete]
        public void DeletePresentationSpeaker(PresentationSpeaker presentationSpeaker)
        {
            if ((presentationSpeaker.EntityState == EntityState.Detached))
            {
                this.ObjectContext.PresentationSpeakers.Attach(presentationSpeaker);
            }
            this.ObjectContext.PresentationSpeakers.DeleteObject(presentationSpeaker);
        }
    }
}


