using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class EventAttendee
	{
		public EventAttendee()
		{
		}

		#region fields
		private Int32 _id;
		private Boolean _checkedIn;
		private String _raffleTicket;
		private ICollection<EventAttendee> _events;
		private ICollection<EventAttendee> _persons;
		private ICollection<SessionAttendee> _sessionAttendances;
		private ICollection<PreferenceValue> _preferenceValues;
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
		public virtual Boolean CheckedIn
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
		public virtual String RaffleTicket
		{
			get
			{
				return _raffleTicket;
			}
			set
			{
				_raffleTicket=value;
			}
		}
		public virtual ICollection<EventAttendee> Events
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
		public virtual ICollection<EventAttendee> Persons
		{
			get
			{
				return _persons;
			}
			set
			{
				_persons=value;
			}
		}
		public virtual ICollection<SessionAttendee> SessionAttendances
		{
			get
			{
				return _sessionAttendances;
			}
			set
			{
				_sessionAttendances=value;
			}
		}
		public virtual ICollection<PreferenceValue> PreferenceValues
		{
			get
			{
				return _preferenceValues;
			}
			set
			{
				_preferenceValues=value;
			}
		}
		#endregion

	}
}
