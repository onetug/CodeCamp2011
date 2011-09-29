
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies EventOrganizerMetadata as the class
    // that carries additional metadata for the EventOrganizer class.
    [MetadataTypeAttribute(typeof(EventOrganizer.EventOrganizerMetadata))]
    public partial class EventOrganizer
    {

        // This class allows you to attach custom attributes to properties
        // of the EventOrganizer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EventOrganizerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EventOrganizerMetadata()
            {
            }

            public Nullable<int> DummyField { get; set; }
            [Include]
            public Event Event { get; set; }
            [Key]
            public int EventsAsOrganizer_Id { get; set; }
            [Key]
            public int Organizers_Id { get; set; }

            public Person Person { get; set; }
        }
    }
}
