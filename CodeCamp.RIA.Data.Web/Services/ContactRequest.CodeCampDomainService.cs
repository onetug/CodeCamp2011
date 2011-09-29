
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
        // To support paging you will need to add ordering to the 'ContactRequests' query.
        [Query(IsDefault = true)]
        public IQueryable<ContactRequest> GetContactRequests()
        {
            return this.ObjectContext.ContactRequests;
        }

        [Query]
        public IQueryable<ContactRequest> GetContactRequestsForEvent(int id)
        {
            return this.ObjectContext.ContactRequests.Include("Event").Where(cr => cr.EventId == id);
        }

        public ContactRequest GetContactRequest(int id)
        {
            return this.ObjectContext.ContactRequests.Where(cr => cr.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertContactRequest(ContactRequest contactRequest)
        {
            if ((contactRequest.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(contactRequest, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ContactRequests.AddObject(contactRequest);
            }
        }
        [Update]
        public void UpdateContactRequest(ContactRequest currentContactRequest)
        {
            this.ObjectContext.ContactRequests.AttachAsModified(currentContactRequest, this.ChangeSet.GetOriginal(currentContactRequest));
        }
        [Delete]
        public void DeleteContactRequest(ContactRequest contactRequest)
        {
            if ((contactRequest.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ContactRequests.Attach(contactRequest);
            }
            this.ObjectContext.ContactRequests.DeleteObject(contactRequest);
        }
    }
}


