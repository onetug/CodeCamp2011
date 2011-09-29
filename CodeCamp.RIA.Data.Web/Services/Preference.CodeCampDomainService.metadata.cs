
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


    // The MetadataTypeAttribute identifies PreferenceMetadata as the class
    // that carries additional metadata for the Preference class.
    [MetadataTypeAttribute(typeof(Preference.PreferenceMetadata))]
    public partial class Preference
    {

        // This class allows you to attach custom attributes to properties
        // of the Preference class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PreferenceMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PreferenceMetadata()
            {
            }

            public Event Event { get; set; }

            [Required]
            public int EventId { get; set; }
            [Key]
            public int Id { get; set; }

            [Include]
            public EntityCollection<PreferenceValue> PreferenceValues { get; set; }
            [Required]
            public string Question { get; set; }
        }
    }
}
