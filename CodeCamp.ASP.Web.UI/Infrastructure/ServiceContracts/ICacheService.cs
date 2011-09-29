namespace CodeCamp.ASP.Web.UI.Infrastructure
{
    using CodeCamp.RIA.Data.Web;
    using System.Collections.Generic;

    public interface ICacheService
    {
        IList<Announcement> FindAnnouncements();
        IList<Presentation> FindPresentations();
        IList<Sponsor> FindSponsors();
        IList<Session> FindSessions();
        bool IsInitialized { get; }
        IList<Announcement> FindAllAnnouncements();
    }
}
