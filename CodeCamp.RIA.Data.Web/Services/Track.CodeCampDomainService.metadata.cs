
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


    // The MetadataTypeAttribute identifies TrackMetadata as the class
    // that carries additional metadata for the Track class.
    [MetadataTypeAttribute(typeof(Track.TrackMetadata))]
    public partial class Track
    {

        // This class allows you to attach custom attributes to properties
        // of the Track class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TrackMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private TrackMetadata()
            {
            }
            [Include]
            public Event Event { get; set; }

            public int EventId { get; set; }

            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }

            [Include]
            [Association("TrackOwners", "Id", "Owners_Id")]
            public EntityCollection<TrackOwner> TrackOwners { get; set; }

            public int RoomId { get; set; }
            [Include]
            public EntityCollection<Session> Sessions { get; set; }
        }
    }
}
