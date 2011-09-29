
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
    [EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'People' query.
        [Query(IsDefault = true)]
        public IQueryable<Person> GetPeople()
        {
            return this.ObjectContext.People.OrderBy(p => p.Name);
        }
        //[OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public Person GetPerson(int id)
        {
            return this.ObjectContext.People.Include("EventAttendees").Include("EventAttendees.EventAttendeePreferenceValues").Where(p => p.Id == id).FirstOrDefault();
        }

        public Person GetPersonByEmail(string email)
        {
            return this.ObjectContext.People.Include("EventAttendees").Where(p => p.Email == email).FirstOrDefault();
        }

        public Person GetPersonWithPresentations(int personId)
        {
            return this.ObjectContext.People
                .Include("PresentationSpeakers")
                .Include("PresentationSpeakers.Presentation")
                .Include("PresentationSpeakers.Presentation.EventPresentations")
                .Where(p => p.Id == personId).FirstOrDefault();
        }
        public Person GetPersonWithPreferencesForEvent(int personId, int eventId)
        {
            return ObjectContext.People
                .Include("EventAttendees")
                .Include("EventAttendees.Event")
                .Include("EventAttendees.EventAttendeePreferenceValues")
                .Include("EventAttendees.EventAttendeePreferenceValues.PreferenceValue")
                .Include("EventAttendees.EventAttendeePreferenceValues.PreferenceValue.Preference")
                .Where(p => p.Id == personId).Where(p => p.EventAttendees.FirstOrDefault().EventId == eventId).FirstOrDefault();
            //return people.Select( p => p.EventAttendees.FirstOrDefault().EventId == eventId) as Person;
        }
        [Insert]
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

        [Update]
        public void UpdatePerson(Person currentPerson)
        {

            this.ObjectContext.People.AttachAsModified(currentPerson, ChangeSet.GetOriginal(currentPerson));
            
        }
        [Delete]
        public void DeletePerson(Person person)
        {
            if ((person.EntityState == EntityState.Detached))
            {
                this.ObjectContext.People.Attach(person);
            }
            this.ObjectContext.People.DeleteObject(person);
        }

        [Invoke]
        public void ResetPeoplePassword(Person passedInPerson)
        {
            this.ObjectContext.People.AttachAsModified(passedInPerson);
            this.ObjectContext.SaveChanges();
        }
    }
}


