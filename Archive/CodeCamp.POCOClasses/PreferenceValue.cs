using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class PreferenceValue
	{
		public PreferenceValue()
		{
		}

		#region fields
		private Int32 _id;
		private ICollection<PreferenceValue> _preferences;
		private String _answer;
		private ICollection<PreferenceValue> _eventAttendees;
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
		public virtual ICollection<PreferenceValue> Preferences
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
		public virtual String Answer
		{
			get
			{
				return _answer;
			}
			set
			{
				_answer=value;
			}
		}
		public virtual ICollection<PreferenceValue> EventAttendees
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
		#endregion

	}
}
