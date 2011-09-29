
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
        // To support paging you will need to add ordering to the 'Sessions' query.
        [Query(IsDefault = true)]
        public IQueryable<Session> GetSessions()
        {
            return this.ObjectContext.Sessions.Include("Room").Include("SessionAttendees").Include("EventPresentation").Include("EventPresentation.Presentation");
        }

        /// <summary>
        /// Use this one. 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Query]
        public IQueryable<Session> GetSessionsByEvent(int eventId)
        {
            return this.ObjectContext.Sessions
                .Include("EventPresentation")
                .Include("EventPresentation.Presentation")
                .Include("Track")
                .Include("EventPresentation.Presentation.PresentationSpeakers.Person")
                .Where(s => s.Track.EventId == eventId);
        }

        [Query]
        public IQueryable<Session> GetPresenterSessionsByEvent(int eventId)
        {
            return this.ObjectContext.Sessions
                .Include("EventPresentation")
                .Include("EventPresentation.Presentation")
                .Include("Track")
                .Include("EventPresentation.Presentation.PresentationSpeakers.Person")
                .Where(s => s.Track.EventId == eventId && s.Status == "Confirmed").OrderBy(s => s.EventPresentation
                                                                                                 .Presentation
                                                                                                 .PresentationSpeakers
                                                                                                 .FirstOrDefault()
                                                                                                 .Person
                                                                                                 .Name);
        }

        [Query]
        public IQueryable<Session> GetExistingPresentionsByEvent(int eventId)
        {
            return this.ObjectContext.Sessions
                .Include("EventPresentation")
                .Include("EventPresentation.Presentation")
                .Include("EventPresentation.Presentation.Files")
                .Include("EventPresentation.Presentation.PresentationSpeakers.Person")
                .Where(s => s.Track.EventId == eventId &&
                       s.Status == "Confirmed" &&
                       s.EventPresentation.Presentation.Files.Count > 0)
                .OrderBy(s => s.EventPresentation
                                .Presentation
                                .PresentationSpeakers
                                .FirstOrDefault()
                                .Person
                                .Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        //[Query]
        //public IQueryable<Session> GetSessionsForEvent(int eventId)
        //{
        //    return this.ObjectContext.Sessions.Include("Room")
        //        .Include("SessionAttendees")
        //        .Include("SessionAttendees.EventAttendee")
        //        .Include("SessionAttendees.EventAttendee.Person")
        //        .Include("EventPresentation")
        //        .Include("EventPresentation.Presentation")
        //        .Include("EventPresentation.Presentation.PresentationSpeakers")
        //        .Where(s=>s.EventPresentation.EventId == eventId);
        //}

        public Session GetSession(int id)
        {
            return this.ObjectContext.Sessions.Where(s => s.Id == id).FirstOrDefault();
        }

        [Query]
        public IQueryable<Session> GetVolunteerSessions()
        {
            var allSessions = this.ObjectContext.Sessions.Include("Room").Include("SessionAttendees");
            return allSessions.Where(s => s.Room.Name == "Campus");
        }


        public void InsertSession(Session session)
        {
            if ((session.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(session, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Sessions.AddObject(session);
            }
        }

        public void UpdateSession(Session currentSession)
        {
            this.ObjectContext.Sessions.AttachAsModified(currentSession, this.ChangeSet.GetOriginal(currentSession));
        }

        public void DeleteSession(Session session)
        {
            if ((session.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Sessions.Attach(session);
            }
            this.ObjectContext.Sessions.DeleteObject(session);
        }
    }
}


