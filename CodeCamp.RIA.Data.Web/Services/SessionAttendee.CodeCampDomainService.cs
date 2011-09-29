
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
    using System.Transactions;

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
        // To support paging you will need to add ordering to the 'SessionAttendees' query.
        [Query(IsDefault = true)]
        public IQueryable<SessionAttendee> GetSessionAttendees()
        {
            return this.ObjectContext.SessionAttendees;
        }
        [Query]
        public IQueryable<SessionAttendee> GetSessionAttendeesForSession(int sessionId)
        {
            return this.ObjectContext.SessionAttendees.Where(s => s.SessionId == sessionId);
        }
        public SessionAttendee GetSessionAttendee(int id)
        {
            return this.ObjectContext.SessionAttendees.Where(s => s.Id == id).FirstOrDefault();
        }

        [Query]
        public IQueryable<SessionAttendee> GetSessionAttendeesFull()
        {
            return this.ObjectContext.SessionAttendees.Include("EventAttendee").Include("Session");
        }

        //[Query]
        //public IQueryable<SessionAttendee> GetSessionAttendeesFullForPerson(int personId)
        //{
        //    return this.ObjectContext.SessionAttendees.Include("Session").Where(s => s.EventAttendeeId == personId);
        //}

        [Query]
        public IQueryable<SessionAttendee> GetAgenda(int eventId, int personId)
        {
            return this.ObjectContext
                .SessionAttendees
                .Include("Session")
                .Where(s => s.Session.Track.EventId == eventId &&  s.EventAttendeeId == personId);
        }

        //[Query]
        //public IQueryable<SessionAttendee> GetSessionAttendeesFullForSession(int sessionId)
        //{
        //    var session = this.ObjectContext.Sessions.Where(s => s.Id == sessionId);
        //    return session.Select(s => s.SessionAttendees) as IQueryable<SessionAttendee>;
        //}

        public bool AssignAttendeeToSession(int sessionId, int personId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // already subscribed?
                if (this.ObjectContext.SessionAttendees.Where(sa => sa.EventAttendeeId == personId && sa.SessionId == sessionId).Count() > 0) return true;

                // over capacity?
                int subscriptionCount = this.ObjectContext.SessionAttendees.Where(sa => sa.SessionId == sessionId).Count();
                int capacity = this.ObjectContext.Sessions.Where(s => s.Id == sessionId).FirstOrDefault().MaxCapacity;
                if (subscriptionCount >= capacity) return false;

                // TODO: check session.status

                // save it
                var sessionAttendee = new SessionAttendee()
                {
                    SessionId = sessionId,
                    EventAttendeeId = personId,
                    CheckedIn = "",
                    Comment = "", 
                    Rating = ""
                };

                this.ObjectContext.SessionAttendees.AddObject(sessionAttendee);
                this.ObjectContext.SaveChanges();

                scope.Complete();

                return true;
            }
        }

        public void DropSessionAttendee(int sessionId, int personId)
        {
            SessionAttendee sa = this.ObjectContext.SessionAttendees.Where(s => s.EventAttendeeId == personId && s.SessionId == sessionId).FirstOrDefault();

            if (sa != null)
            {
                if ((sa.EntityState == EntityState.Detached))
                {
                    this.ObjectContext.SessionAttendees.Attach(sa);
                }
                this.ObjectContext.SessionAttendees.DeleteObject(sa);
                this.ObjectContext.SaveChanges();
            }            
        }

        public void InsertSessionAttendee(SessionAttendee sessionAttendee)
        {
            if ((sessionAttendee.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sessionAttendee, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SessionAttendees.AddObject(sessionAttendee);
            }
        }

        public void UpdateSessionAttendee(SessionAttendee currentSessionAttendee)
        {
            this.ObjectContext.SessionAttendees.AttachAsModified(currentSessionAttendee, this.ChangeSet.GetOriginal(currentSessionAttendee));
        }

        public void DeleteSessionAttendee(SessionAttendee sessionAttendee)
        {
            if ((sessionAttendee.EntityState == EntityState.Detached))
            {
                this.ObjectContext.SessionAttendees.Attach(sessionAttendee);
            }
            this.ObjectContext.SessionAttendees.DeleteObject(sessionAttendee);
        }
    }
}


