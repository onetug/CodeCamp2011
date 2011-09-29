using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Event
	{
		public Event()
		{
		}

		#region fields
		private Int32 _id;
		private ICollection<Track> _tracks;
		private ICollection<Person> _organizers;
		private String _name;
		private String _description;
		private ICollection<Location> _locations;
		private ICollection<Sponsor> _sponsors;
		private ICollection<EventAttendee> _attendees;
		private ICollection<EventPresentation> _presentations;
		private ICollection<Announcement> _announcements;
		private ICollection<ContactRequest> _contactRequests;
		private String _twitterTag;
		private ICollection<Preference> _preferences;
		#endregion

		#region properties
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
		public virtual ICollection<Track> Tracks
		{
			get
			{
				return _tracks;
			}
			set
			{
				_tracks=value;
			}
		}
		public virtual ICollection<Person> Organizers
		{
			get
			{
				return _organizers;
			}
			set
			{
				_organizers=value;
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
		public virtual String Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description=value;
			}
		}
		public virtual ICollection<Location> Locations
		{
			get
			{
				return _locations;
			}
			set
			{
				_locations=value;
			}
		}
		public virtual ICollection<Sponsor> Sponsors
		{
			get
			{
				return _sponsors;
			}
			set
			{
				_sponsors=value;
			}
		}
		public virtual ICollection<EventAttendee> Attendees
		{
			get
			{
				return _attendees;
			}
			set
			{
				_attendees=value;
			}
		}
		public virtual ICollection<EventPresentation> Presentations
		{
			get
			{
				return _presentations;
			}
			set
			{
				_presentations=value;
			}
		}
		public virtual ICollection<Announcement> Announcements
		{
			get
			{
				return _announcements;
			}
			set
			{
				_announcements=value;
			}
		}
		public virtual ICollection<ContactRequest> ContactRequests
		{
			get
			{
				return _contactRequests;
			}
			set
			{
				_contactRequests=value;
			}
		}
		public virtual String TwitterTag
		{
			get
			{
				return _twitterTag;
			}
			set
			{
				_twitterTag=value;
			}
		}
		public virtual ICollection<Preference> Preferences
		{
			get
			{
				return _preferences;
			}
			set
			{
				_preferences=value;
			}
		}
		#endregion

	}
}
