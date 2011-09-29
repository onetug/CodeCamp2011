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
    public class RegistrationService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Events' query.
        public IQueryable<Event> GetEvents()
        {
            return this.ObjectContext.Events;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'EventAttendees' query.
        public IQueryable<EventAttendee> GetEventAttendees()
        {
            return this.ObjectContext.EventAttendees;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'People' query.
        public IQueryable<Person> GetPeople()
        {
            return this.ObjectContext.People;
        }

        public void SavePerson(Person person)
        {
            this.ObjectContext.AddToPeople(person);
            this.ObjectContext.SaveChanges();            
        }

        public void InsertPerson(Person person)
        {
            if ((person.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(person, EntityState.Added);
            }
            else
            {
                this.ObjectContext.People.AddObject(person);
            }
        }

        public void UpdatePerson(Person currentPerson)
        {
            this.ObjectContext.People.AttachAsModified(currentPerson, this.ChangeSet.GetOriginal(currentPerson));
        }

        public void DeletePerson(Person person)
        {
            if ((person.EntityState == EntityState.Detached))
            {
                this.ObjectContext.People.Attach(person);
            }
            this.ObjectContext.People.DeleteObject(person);
        }

        public Person GetPerson(long id)
        {
            return this.ObjectContext.People.Where(s => s.Id == id).SingleOrDefault();
        }
    }
}


