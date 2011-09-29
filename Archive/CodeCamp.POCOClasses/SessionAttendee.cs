using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class SessionAttendee
	{
		public SessionAttendee()
		{
		}

		#region fields
		private Int32 _id;
		private ICollection<SessionAttendee> _eventAttendees;
		private String _checkedIn;
		private String _rating;
		private String _comment;
		private ICollection<SessionAttendee> _sessions;
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
		public virtual ICollection<SessionAttendee> EventAttendees
		{
			get
			{
				return _eventAttendees;
			}
			set
			{
				_eventAttendees=value;
			}
		}
		public virtual String CheckedIn
		{
			get
			{
				return _checkedIn;
			}
			set
			{
				_checkedIn=value;
			}
		}
		public virtual String Rating
		{
			get
			{
				return _rating;
			}
			set
			{
				_rating=value;
			}
		}
		public virtual String Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				_comment=value;
			}
		}
		public virtual ICollection<SessionAttendee> Sessions
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
		#endregion

	}
}
