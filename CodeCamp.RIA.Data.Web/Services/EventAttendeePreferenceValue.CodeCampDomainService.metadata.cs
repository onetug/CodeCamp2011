
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies EventAttendeePreferenceValueMetadata as the class
    // that carries additional metadata for the EventAttendeePreferenceValue class.
    [MetadataTypeAttribute(typeof(EventAttendeePreferenceValue.EventAttendeePreferenceValueMetadata))]
    public partial class EventAttendeePreferenceValue
    {

        // This class allows you to attach custom attributes to properties
        // of the EventAttendeePreferenceValue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EventAttendeePreferenceValueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EventAttendeePreferenceValueMetadata()
            {
            }

            public Nullable<int> DummyField { get; set; }

            [Include]
            public EventAttendee EventAttendee { get; set; }

            [Key]
            public int Id { get; set; }

            public int EventAttendees_Id { get; set; }
            [Include]
            public PreferenceValue PreferenceValue { get; set; }

            public int PreferenceValues_Id { get; set; }
        }
    }
}
