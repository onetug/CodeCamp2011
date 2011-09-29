using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class EventPresentation
	{
		public EventPresentation()
		{
		}

		#region fields
		private Int32 _id;
		private ICollection<Presentation> _presentations;
		private String _approvalStatus;
		private ICollection<EventPresentation> _events;
		private Session _session;
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
		public virtual ICollection<Presentation> Presentations
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
		public virtual String ApprovalStatus
		{
			get
			{
				return _approvalStatus;
			}
			set
			{
				_approvalStatus=value;
			}
		}
		public virtual ICollection<EventPresentation> Events
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
		public virtual Session Session
		{
			get
			{
				return _session;
			}
			set
			{
				_session=value;
			}
		}
		#endregion

	}
}
