
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
        // To support paging you will need to add ordering to the 'Presentations' query.
        [Query(IsDefault = true)]
        public IQueryable<Presentation> GetPresentations()
        {
            return this.ObjectContext.Presentations;
        }
        [Query]
        public IQueryable<Presentation> GetPresentationsFull()
        {
            return this.ObjectContext.Presentations.Include("EventPresentations").Include("PresentationSpeakers").Include("PresentationSpeakers.Person");
        }
        public Presentation GetPresentation(int id)
        {
            return this.ObjectContext.Presentations.Where(p => p.Id == id).FirstOrDefault();
        }

        [Insert]
        public void InsertPresentation(Presentation presentation)
        {
            if ((presentation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(presentation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Presentations.AddObject(presentation);
            }
        }
        [Update]
        public void UpdatePresentation(Presentation currentPresentation)
        {
            this.ObjectContext.Presentations.AttachAsModified(currentPresentation, this.ChangeSet.GetOriginal(currentPresentation));
        }
        [Delete]
        public void DeletePresentation(Presentation presentation)
        {
            if ((presentation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Presentations.Attach(presentation);
            }

            this.ObjectContext.Presentations.DeleteObject(presentation);
        }
    }
}


