using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodeCamp.WP7.Tools
{
    public static class Mapper
    {
        public static Model.Session ToModelSession(this AgendaServiceRef.Session session)
        {
            return new Model.Session() 
            { 
                Id = session.Id,
                Name = session.Name,
                Description = session.Description,
                Track = session.Track,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Speaker = session.Speaker,
                Room = session.Room,
                Level = session.Level
            };
        }

        public static Model.Speaker ToModelSpeaker(this AgendaServiceRef.Speaker speaker)
        {
            return new Model.Speaker()
            {
                Name = speaker.Name,
                Title = speaker.Title,
                Blog = speaker.Blog,
                Twitter = speaker.Twitter,
                Bio = speaker.Bio
            };
        }

        public static Model.Track ToModelTrack(this AgendaServiceRef.Track track)
        {
            return new Model.Track()
            {
                Name = track.Name
            };
        }

        public static Model.AgendaItem ToModelAgendaItem(this AgendaServiceRef.AgendaItem item)
        {
            return new Model.AgendaItem()
            {
                SessionId = item.SessionId,
                Rating = item.Rating,
                Comment = item.Comment
            };
        }
    }
}