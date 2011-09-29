
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


    // The MetadataTypeAttribute identifies RoomMetadata as the class
    // that carries additional metadata for the Room class.
    [MetadataTypeAttribute(typeof(Room.RoomMetadata))]
    public partial class Room
    {

        // This class allows you to attach custom attributes to properties
        // of the Room class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RoomMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RoomMetadata()
            {
            }

            public string Description { get; set; }
            [Key]
            public int Id { get; set; }

            public Location Location { get; set; }

            [Required]
            public int LocationId { get; set; }

            public int MaxCapacity { get; set; }
            [Required]
            public string Name { get; set; }

            public EntityCollection<Session> Sessions { get; set; }
        }
    }
}
