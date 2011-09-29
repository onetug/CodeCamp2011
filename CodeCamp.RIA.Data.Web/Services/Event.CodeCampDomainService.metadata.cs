
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies EventMetadata as the class
    // that carries additional metadata for the Event class.
    [MetadataTypeAttribute(typeof(Event.EventMetadata))]
    public partial class Event
    {

        // This class allows you to attach custom attributes to properties
        // of the Event class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EventMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EventMetadata()
            {
            }
            [Include]
            public EntityCollection<Announcement> Announcements { get; set; }
            [Include]
            public EntityCollection<ContactRequest> ContactRequests { get; set; }

            public string Description { get; set; }

            [Include]
            public EntityCollection<EventAttendee> EventAttendees { get; set; }
            [Include]
            public EntityCollection<EventPresentation> EventPresentations { get; set; }

            [Key]
            public int Id { get; set; }

            [Include]
            public Location Location { get; set; }

            public int LocationId { get; set; }

            public string Name { get; set; }

            [Include]
            [Association("EventOrganizers", "Id", "EventsAsOrganizer_Id")]
            public EntityCollection<EventOrganizer> EventOrganizers { get; set; }
            [Include]
            public EntityCollection<Preference> Preferences { get; set; }
            [Include]
            public EntityCollection<Sponsor> Sponsors { get; set; }
            [Include]
            public EntityCollection<Track> Tracks { get; set; }

            public string TwitterTag { get; set; }
        }
    }
}
