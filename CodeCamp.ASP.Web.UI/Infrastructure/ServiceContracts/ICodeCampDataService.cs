namespace CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts
{
    using System.Collections.Generic;
    using CodeCamp.RIA.Data.Web;

    public interface ICodeCampDataService
    {
        void ConfigureCurrentEvent(long id);

        void AddPerson(Person person);

        Person FindPersonByEmail(Person person);

        IList<Announcement> FindAllAnnouncements();

        IList<Presentation> FindAllPresentations();

        IList<Track> FindAllTracks();

        IList<Sponsor> FindAllSponsors(SponsorshipLevel sponsorshipLevel);

        IList<Session> FindAllSessions();
    }
}