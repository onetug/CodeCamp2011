
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies TrackOwnerMetadata as the class
    // that carries additional metadata for the TrackOwner class.
    [MetadataTypeAttribute(typeof(TrackOwner.TrackOwnerMetadata))]
    public partial class TrackOwner
    {

        // This class allows you to attach custom attributes to properties
        // of the TrackOwner class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TrackOwnerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private TrackOwnerMetadata()
            {
            }

            public Nullable<int> DummyField { get; set; }
            [Key]
            public int Owners_Id { get; set; }
            [Include]
            public Person Person { get; set; }
            [Include]
            public Track Track { get; set; }
            [Key]
            public int TracksAsOwner_Id { get; set; }
        }
    }
}
