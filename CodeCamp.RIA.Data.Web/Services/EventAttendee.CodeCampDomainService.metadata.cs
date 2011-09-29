
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


    // The MetadataTypeAttribute identifies EventAttendeeMetadata as the class
    // that carries additional metadata for the EventAttendee class.
    [MetadataTypeAttribute(typeof(EventAttendee.EventAttendeeMetadata))]
    public partial class EventAttendee
    {

        // This class allows you to attach custom attributes to properties
        // of the EventAttendee class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EventAttendeeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EventAttendeeMetadata()
            {
            }

            public bool CheckedIn { get; set; }

            [Include]
            public Event Event { get; set; }
            [Required]
            public int EventId { get; set; }
            [Key]
            public int Id { get; set; }

            [Include]
            public Person Person { get; set; }
            [Required]
            public int PersonId { get; set; }

            [Include]
            [Association("EventAttendeePreferenceValues", "Id","EventAttendees_Id" )]
            public EntityCollection<PreferenceValue> EventAttendeePreferenceValues { get; set; }

            public string RaffleTicket { get; set; }
            [Include]
            public EntityCollection<SessionAttendee> SessionAttendees { get; set; }
        }
    }
}
