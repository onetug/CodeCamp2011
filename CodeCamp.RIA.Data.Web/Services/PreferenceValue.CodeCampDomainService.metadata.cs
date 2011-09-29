
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


    // The MetadataTypeAttribute identifies PreferenceValueMetadata as the class
    // that carries additional metadata for the PreferenceValue class.
    [MetadataTypeAttribute(typeof(PreferenceValue.PreferenceValueMetadata))]
    public partial class PreferenceValue
    {

        // This class allows you to attach custom attributes to properties
        // of the PreferenceValue class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PreferenceValueMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PreferenceValueMetadata()
            {
            }
            [Required]
            public string Answer { get; set; }

            [Include]
            [Association("EventAttendeePreferenceValues", "Id", "PreferenceValues_Id")]
            public EntityCollection<EventAttendeePreferenceValue> EventAttendeePreferenceValues { get; set; }

            [Key]
            public int Id { get; set; }
            [Include]
            public Preference Preference { get; set; }

            public int PreferenceId { get; set; }
        }
    }
}
