
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


    // The MetadataTypeAttribute identifies SessionMetadata as the class
    // that carries additional metadata for the Session class.
    [MetadataTypeAttribute(typeof(Session.SessionMetadata))]
    public partial class Session
    {

        // This class allows you to attach custom attributes to properties
        // of the Session class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SessionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SessionMetadata()
            {
            }
            
            public string Description { get; set; }

            public DateTime EndTime { get; set; }

            [Include]
            public EventPresentation EventPresentation { get; set; }
            [Required]
            public int EventPresentation_Id { get; set; }

            [Key]
            public int Id { get; set; }

            public int MaxCapacity { get; set; }
            
            [Required]
            public string Name { get; set; }

            [Include]
            public Room Room { get; set; }

            public int RoomId { get; set; }

            [Include]
            [Association("SessionAttendees", "Id","SessionId")]
            public EntityCollection<SessionAttendee> SessionAttendees { get; set; }

            [Required]
            public string SessionType { get; set; }

            public DateTime StartTime { get; set; }

            public string Status { get; set; }

            [Include]
            public Track Track { get; set; }

            public int TrackId { get; set; }
        }
    }
}
