
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


    // The MetadataTypeAttribute identifies PresentationMetadata as the class
    // that carries additional metadata for the Presentation class.
    [MetadataTypeAttribute(typeof(Presentation.PresentationMetadata))]
    public partial class Presentation
    {

        // This class allows you to attach custom attributes to properties
        // of the Presentation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PresentationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PresentationMetadata()
            {
            }
            [Required(ErrorMessage = "You must provide a description.")]
            [Display(Name = "Description:", Description = "A detailed description of what you intend to present.")]
            public string Description { get; set; }

            [Include]
            //[Composition]
            [Association("EventPresentations", "Id", "PresentationId")]
            public EntityCollection<EventPresentation> EventPresentations { get; set; }
            [Include]
            public EntityCollection<File> Files { get; set; }
            [Key]
            public int Id { get; set; }
            [Required]
            public string Level { get; set; }
            [Display(Name = "Title:", Description = "The Presentation Title")]
            [Required(ErrorMessage = "You must provide a title.")]
            public string Name { get; set; }

            [Include]
            //[Composition]
            [Association("SpeakerPresentations", "Id", "PresentationsAsSpeaker_Id")]
            public EntityCollection<PresentationSpeaker> PresentationSpeakers { get; set; }

            //[Include]
            //[Association("Speakers", "Id", "Id", IsForeignKey = false)]
            //public EntityCollection<Person> Speakers { get; set; }
        }
    }
}
