using System.Data.Objects.DataClasses;

namespace CodeCamp.ASP.UI.Infrastructure
{
    using CodeCamp.RIA.Data.Web;
    using System.Collections.Generic;

    public interface ICacheService
    {
        EntityCollection<Announcement> FindAllAnnouncements();
        EntityCollection<EventPresentation> FindPresentations();
        EntityCollection<Sponsor> FindSponsors();
        IList<Session> FindSessions();
        bool IsInitialized { get; }
    }
}
