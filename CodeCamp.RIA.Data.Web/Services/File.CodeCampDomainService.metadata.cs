
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies FileMetadata as the class
    // that carries additional metadata for the File class.
    [MetadataTypeAttribute(typeof(File.FileMetadata))]
    public partial class File
    {

        // This class allows you to attach custom attributes to properties
        // of the File class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FileMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private FileMetadata()
            {
            }
            [Required]
            public string Description { get; set; }

            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }

            public Presentation Presentation { get; set; }

            public int PresentationId { get; set; }
            [Required]
            public string Type { get; set; }
        }
    }
}
