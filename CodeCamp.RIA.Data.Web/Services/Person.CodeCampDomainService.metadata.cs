
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


    // The MetadataTypeAttribute identifies PersonMetadata as the class
    // that carries additional metadata for the Person class.
    [MetadataTypeAttribute(typeof(Person.PersonMetadata))]
    public partial class Person
    {

        // This class allows you to attach custom attributes to properties
        // of the Person class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PersonMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PersonMetadata()
            {
            }

            public EntityCollection<Announcement> Announcements { get; set; }

            public string Bio { get; set; }

            public string Blog { get; set; }

            [Required(ErrorMessage = "Email Address is required.")]
            //[RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "{0} must be a Valid Email Address")]
            //[Bindable(true, BindingDirection.TwoWay)]
            [Display(Name = "Email Address", Description = "Your Email Address.")]
            public string Email { get; set; }

            [Include]
            [Association("EventAttendees","Id","PersonId")]
            public EntityCollection<EventAttendee> EventAttendees { get; set; }

            [Include]
            [Association("EventOwners", "Id", "Organizers_Id")]
            public EntityCollection<EventOrganizer> EventOrganizers { get; set; }

            [Required(ErrorMessage = "First Name is required.")]
            public string FirstName { get; set; }

            [Key]
            public int Id { get; set; }

            public string ImageUrl { get; set; }

            [Required(ErrorMessage = "Last Name is required.")]
            public string LastName { get; set; }

            public string Name { get; set; }

            public string PasswordHash { get; set; }

            [Include]
            [Association("SpeakerPresentations", "Id", "Speakers_Id")]
            public EntityCollection<PresentationSpeaker> PresentationSpeakers { get; set; }

            [Include]
            [Association("SponsorPersons", "Id", "Owners_Id")]
            public EntityCollection<SponsorPerson> SponsorPersons { get; set; }

            public string Title { get; set; }

            [Include]
            [Association("TrackOwners", "Id", "TracksAsOwner_Id")]
            public EntityCollection<TrackOwner> TrackOwners { get; set; }

            public string Twitter { get; set; }

            public string Website { get; set; }
        }
    }
}
