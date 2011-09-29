using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Track
	{
		public Track()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private ICollection<Track> _events;
		private ICollection<Session> _sessions;
		private ICollection<Person> _owners;
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
		public virtual ICollection<Track> Events
		{
			get
			{
				return _events;
			}
			set
			{
				_events=value;
			}
		}
		public virtual ICollection<Session> Sessions
		{
			get
			{
				return _sessions;
			}
			set
			{
				_sessions=value;
			}
		}
		public virtual ICollection<Person> Owners
		{
			get
			{
				return _owners;
			}
			set
			{
				_owners=value;
			}
		}
		#endregion

	}
}
