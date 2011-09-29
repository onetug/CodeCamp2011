
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies SponsorPersonMetadata as the class
    // that carries additional metadata for the SponsorPerson class.
    [MetadataTypeAttribute(typeof(SponsorPerson.SponsorPersonMetadata))]
    public partial class SponsorPerson
    {

        // This class allows you to attach custom attributes to properties
        // of the SponsorPerson class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SponsorPersonMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SponsorPersonMetadata()
            {
            }

            public Nullable<int> DummyField { get; set; }
            [Key]
            public int Owners_Id { get; set; }
            [Include]
            public Person Person { get; set; }
            [Include]
            public Sponsor Sponsor { get; set; }
            [Key]
            public int SponsorsAsOwner_Id { get; set; }
        }
    }
}
