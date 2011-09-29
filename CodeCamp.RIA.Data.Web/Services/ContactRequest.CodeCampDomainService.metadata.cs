
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies ContactRequestMetadata as the class
    // that carries additional metadata for the ContactRequest class.
    [MetadataTypeAttribute(typeof(ContactRequest.ContactRequestMetadata))]
    public partial class ContactRequest
    {

        // This class allows you to attach custom attributes to properties
        // of the ContactRequest class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ContactRequestMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ContactRequestMetadata()
            {
            }

            public string Category { get; set; }

            public string Description { get; set; }

            [Required]
            public string Email { get; set; }

            [Include]
            public Event Event { get; set; }

            public int EventId { get; set; }

            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }

            public string Status { get; set; }
            [Required]
            public string Subject { get; set; }
        }
    }
}
