using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Preference
	{
		public Preference()
		{
		}

		#region fields
		private Int32 _id;
		private ICollection<Preference> _events;
		private String _question;
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
		public virtual ICollection<Preference> Events
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
		public virtual String Question
		{
			get
			{
				return _question;
			}
			set
			{
				_question=value;
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
