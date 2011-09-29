using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Announcement
	{
		public Announcement()
		{
		}

		#region fields
		private Int32 _id;
		private String _title;
		private String _text;
		private ICollection<Announcement> _authors;
		private DateTime _publishDate;
		private ICollection<Announcement> _events;
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
		public virtual String Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text=value;
			}
		}
		public virtual ICollection<Announcement> Authors
		{
			get
			{
				return _authors;
			}
			set
			{
				_authors=value;
			}
		}
		public virtual DateTime PublishDate
		{
			get
			{
				return _publishDate;
			}
			set
			{
				_publishDate=value;
			}
		}
		public virtual ICollection<Announcement> Events
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
		#endregion

	}
}
