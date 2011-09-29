
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


    // The MetadataTypeAttribute identifies LocationMetadata as the class
    // that carries additional metadata for the Location class.
    [MetadataTypeAttribute(typeof(Location.LocationMetadata))]
    public partial class Location
    {

        // This class allows you to attach custom attributes to properties
        // of the Location class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LocationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private LocationMetadata()
            {
            }
            [Required]
            public string Address1 { get; set; }

            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }

            public EntityCollection<Event> Events { get; set; }
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }

            public EntityCollection<Room> Rooms { get; set; }
            [Required]
            public string State { get; set; }
            [Required]
            public string Zip { get; set; }
        }
    }
}
