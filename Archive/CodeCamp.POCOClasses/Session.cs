using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Session
	{
		public Session()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _description;
		private ICollection<Session> _tracks;
		private DateTime _startTime;
		private DateTime _endTime;
		private String _sessionType;
		private EventPresentation _eventPresentation;
		private ICollection<Room> _rooms;
		private ICollection<SessionAttendee> _sessionAttendees;
		private Int32 _maxCapacity;
		private String _status;
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
		public virtual ICollection<Session> Tracks
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
		public virtual DateTime StartTime
		{
			get
			{
				return _startTime;
			}
			set
			{
				_startTime=value;
			}
		}
		public virtual DateTime EndTime
		{
			get
			{
				return _endTime;
			}
			set
			{
				_endTime=value;
			}
		}
		public virtual String SessionType
		{
			get
			{
				return _sessionType;
			}
			set
			{
				_sessionType=value;
			}
		}
		public virtual EventPresentation EventPresentation
		{
			get
			{
				return _eventPresentation;
			}
			set
			{
				_eventPresentation=value;
			}
		}
		public virtual ICollection<Room> Rooms
		{
			get
			{
				return _rooms;
			}
			set
			{
				_rooms=value;
			}
		}
		public virtual ICollection<SessionAttendee> SessionAttendees
		{
			get
			{
				return _sessionAttendees;
			}
			set
			{
				_sessionAttendees=value;
			}
		}
		public virtual Int32 MaxCapacity
		{
			get
			{
				return _maxCapacity;
			}
			set
			{
				_maxCapacity=value;
			}
		}
		public virtual String Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status=value;
			}
		}
		#endregion

	}
}
