namespace CodeCamp.ASP.UI.Infrastructure.ServiceContracts
{
    using System.Collections.Generic;
    using CodeCamp.RIA.Data.Web;

    public interface ICodeCampDataService
    {
        void ConfigureCurrentEvent(long id);

        void AddPerson(Person person);

        Event CurrentEvent { get; }

        Person FindPersonByEmail(Person person);

        IList<Announcement> FindAllAnnouncements();

        IList<Presentation> FindAllPresentations();

        IList<Track> FindAllTracks();

        IList<Sponsor> FindAllSponsors(SponsorshipLevel sponsorshipLevel);

        IList<Session> FindAllSessions();

        Person FindPersonByEmail(string emailAddress);
        
        void ResetAttendeePassword(Person person);
    }
}