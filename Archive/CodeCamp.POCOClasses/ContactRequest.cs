using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class ContactRequest
	{
		public ContactRequest()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _subject;
		private String _description;
		private String _category;
		private String _email;
		private ICollection<ContactRequest> _events;
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
		public virtual String Subject
		{
			get
			{
				return _subject;
			}
			set
			{
				_subject=value;
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
		public virtual String Category
		{
			get
			{
				return _category;
			}
			set
			{
				_category=value;
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
		public virtual ICollection<ContactRequest> Events
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
