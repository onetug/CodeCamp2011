
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


    // The MetadataTypeAttribute identifies EventPresentationMetadata as the class
    // that carries additional metadata for the EventPresentation class.
    [MetadataTypeAttribute(typeof(EventPresentation.EventPresentationMetadata))]
    public partial class EventPresentation
    {

        // This class allows you to attach custom attributes to properties
        // of the EventPresentation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EventPresentationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EventPresentationMetadata()
            {
            }

            public string ApprovalStatus { get; set; }

            [Include]
            public Event Event { get; set; }

            public int EventId { get; set; }
            [Key]
            public int Id { get; set; }
            [Include]
            public Presentation Presentation { get; set; }

            public int PresentationId { get; set; }

            public EntityCollection<Session> Sessions { get; set; }
        }
    }
}
