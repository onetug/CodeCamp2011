using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Person:BaseClass
	{
		public Person()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _email;
		private String _title;
		private String _bio;
		private ICollection<Person> _eventsAsOrganizers;
		private ICollection<Announcement> _announcementsAsAuthors;
		private String _website;
		private String _blog;
		private String _twitter;
		private String _passwordHash;
		private ICollection<Person> _presentationsAsSpeakers;
		private ICollection<Person> _sponsorsAsOwners;
		private ICollection<EventAttendee> _eventAttendances;
		private ICollection<Person> _tracksAsOwners;
		#endregion

		#region properties
        [System.ComponentModel.DataAnnotations.Key]
		public virtual Int32 Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id=value;
			}
		}
		public virtual String Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name=value;
			}
		}
		public virtual String Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email=value;
			}
		}
		public virtual String Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title=value;
			}
		}
		public virtual String Bio
		{
			get
			{
				return _bio;
			}
			set
			{
				_bio=value;
			}
		}
		public virtual ICollection<Person> EventsAsOrganizers
		{
			get
			{
				return _eventsAsOrganizers;
			}
			set
			{
				_eventsAsOrganizers=value;
			}
		}
		public virtual ICollection<Announcement> AnnouncementsAsAuthors
		{
			get
			{
				return _announcementsAsAuthors;
			}
			set
			{
				_announcementsAsAuthors=value;
			}
		}
		public virtual String Website
		{
			get
			{
				return _website;
			}
			set
			{
				_website=value;
			}
		}
		public virtual String Blog
		{
			get
			{
				return _blog;
			}
			set
			{
				_blog=value;
			}
		}
		public virtual String Twitter
		{
			get
			{
				return _twitter;
			}
			set
			{
				_twitter=value;
			}
		}
		public virtual String PasswordHash
		{
			get
			{
				return _passwordHash;
			}
			set
			{
				_passwordHash=value;
			}
		}
		public virtual ICollection<Person> PresentationsAsSpeakers
		{
			get
			{
				return _presentationsAsSpeakers;
			}
			set
			{
				_presentationsAsSpeakers=value;
			}
		}
		public virtual ICollection<Person> SponsorsAsOwners
		{
			get
			{
				return _sponsorsAsOwners;
			}
			set
			{
				_sponsorsAsOwners=value;
			}
		}
		public virtual ICollection<EventAttendee> EventAttendances
		{
			get
			{
				return _eventAttendances;
			}
			set
			{
				_eventAttendances=value;
			}
		}
		public virtual ICollection<Person> TracksAsOwners
		{
			get
			{
				return _tracksAsOwners;
			}
			set
			{
				_tracksAsOwners=value;
			}
		}
		#endregion

	}
}
