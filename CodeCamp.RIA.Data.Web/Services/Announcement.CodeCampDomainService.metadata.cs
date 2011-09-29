
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies AnnouncementMetadata as the class
    // that carries additional metadata for the Announcement class.
    [MetadataTypeAttribute(typeof(Announcement.AnnouncementMetadata))]
    public partial class Announcement
    {

        // This class allows you to attach custom attributes to properties
        // of the Announcement class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class AnnouncementMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private AnnouncementMetadata()
            {
            }

            [Include]
            public Person Author { get; set; }

            public int AuthorId { get; set; }

            [Include]
            public Event Event { get; set; }

            [Required]
            public int EventId { get; set; }
            [Key]
            public int Id { get; set; }

            public Nullable<DateTime> PublishDate { get; set; }

            public string Text { get; set; }
            [Required]
            public string Title { get; set; }
        }
    }
}
