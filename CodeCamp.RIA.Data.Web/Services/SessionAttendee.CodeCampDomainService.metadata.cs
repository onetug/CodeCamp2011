
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies SessionAttendeeMetadata as the class
    // that carries additional metadata for the SessionAttendee class.
    [MetadataTypeAttribute(typeof(SessionAttendee.SessionAttendeeMetadata))]
    public partial class SessionAttendee
    {

        // This class allows you to attach custom attributes to properties
        // of the SessionAttendee class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SessionAttendeeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SessionAttendeeMetadata()
            {
            }

            public string CheckedIn { get; set; }

            public string Comment { get; set; }

            [Include]
            [Association("EventAttendee","Id","EventId")]
            public EventAttendee EventAttendee { get; set; }


            public int EventAttendeeId { get; set; }

            [Key]
            public int Id { get; set; }

            public string Rating { get; set; }

            [Include]
            public Session Session { get; set; }

            public int SessionId { get; set; }
        }
    }
}
