namespace CodeCamp.ASP.UI.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CodeCamp.ASP.UI.Infrastructure.Mappers;
    using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
    using CodeCamp.RIA.Data.Web;

    [Serializable]
    public class CodeCampDataService : CodeCampDomainService, ICodeCampDataService
    {
        ISponsorMapper SponsorMapper { get; set; }
        
        IPresentationMapper PresentationMapper { get; set; }

        IAnnouncementMapper AnnouncementMapper { get; set; }

        public Event CurrentEvent { get; set; }

        public void ConfigureCurrentEvent(long id)
        {
            this.CurrentEvent = base.GetEvents().FirstOrDefault(ev => ev.Id == id);
        }

        public CodeCampDataService(ISponsorMapper sponsorMapper, IPresentationMapper presentationMapper, IAnnouncementMapper announcementMapper)
        {
            this.SponsorMapper = sponsorMapper;
            this.PresentationMapper = presentationMapper;
            this.AnnouncementMapper = announcementMapper;
        }

        public void AddPerson(Person person)
        {
            base.InsertPerson(person);
        }

        public Person FindPersonByEmail(Person person)
        {
            return base.GetPersonByEmail(person.Email);
        }

        public IList<Announcement> FindAllAnnouncements()
        {
            return base.GetAnnouncements().ToList();
        }

        public IList<Presentation> FindAllPresentations()
        {
            return base.GetPresentations().ToList();
        }

        public IList<Session> FindAllSessions()
        {
            return base.GetSessions().ToList();
        }

        public IList<Presentation> FindPresentationsByCurrentEvent()
        {
            var currentEventPresentations =
                base.GetEvents().FirstOrDefault(evts => evts.Id == this.CurrentEvent.Id).EventPresentations.ToList();

            return this.PresentationMapper.Map(currentEventPresentations);
        }

        public IList<Track> FindAllTracks()
        {
            return base.GetTracks().ToList();
        }

        public IList<Sponsor> FindAllSponsors(SponsorshipLevel sponsorshipLevel)
        {
            return base.GetSponsors().ToList();
        }

        public Person FindPersonByEmail(string emailAddress)
        {
            return base.GetPersonByEmail(emailAddress);
        }

        public void ResetAttendeePassword(Person person)
        {

            base.ResetPeoplePassword(person);
        }
    }
}