
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


    // The MetadataTypeAttribute identifies SponsorMetadata as the class
    // that carries additional metadata for the Sponsor class.
    [MetadataTypeAttribute(typeof(Sponsor.SponsorMetadata))]
    public partial class Sponsor
    {

        // This class allows you to attach custom attributes to properties
        // of the Sponsor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SponsorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SponsorMetadata()
            {
            }

            public string ApprovalStatus { get; set; }
            [Required]
            public string Description { get; set; }
            [Include]
            public Event Event { get; set; }

            public int EventId { get; set; }
            [Key]
            public int Id { get; set; }

            public string Image { get; set; }
            [Required]
            public string Name { get; set; }

            public string Notes { get; set; }

            [Include]
            [Association("SponsorPersons", "Id", "SponsorsAsOwner_Id")]
            public EntityCollection<SponsorPerson> SponsorPersons { get; set; }
            [Include]
            public SponsorshipLevel SponsorshipLevel { get; set; }

            public int SponsorshipLevelId { get; set; }

            public string Url { get; set; }
        }
    }
}
