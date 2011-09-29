
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies PresentationSpeakerMetadata as the class
    // that carries additional metadata for the PresentationSpeaker class.
    [MetadataTypeAttribute(typeof(PresentationSpeaker.PresentationSpeakerMetadata))]
    public partial class PresentationSpeaker
    {

        // This class allows you to attach custom attributes to properties
        // of the PresentationSpeaker class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PresentationSpeakerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PresentationSpeakerMetadata()
            {
            }

            //public Nullable<int> DummyField { get; set; }

            [Include]
            public Person Person { get; set; }
            [Include]
            public Presentation Presentation { get; set; }

            [Key]
            public int PresentationsAsSpeaker_Id { get; set; }
            [Key]
            public int Speakers_Id { get; set; }
        }
    }
}
