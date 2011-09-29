using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Presentation
	{
		public Presentation()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _description;
		private String _level;
		private ICollection<File> _files;
		private ICollection<Person> _speakers;
		private ICollection<Presentation> _presentationSubmissions;
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
		public virtual String Level
		{
			get
			{
				return _level;
			}
			set
			{
				_level=value;
			}
		}
		public virtual ICollection<File> Files
		{
			get
			{
				return _files;
			}
			set
			{
				_files=value;
			}
		}
		public virtual ICollection<Person> Speakers
		{
			get
			{
				return _speakers;
			}
			set
			{
				_speakers=value;
			}
		}
		public virtual ICollection<Presentation> PresentationSubmissions
		{
			get
			{
				return _presentationSubmissions;
			}
			set
			{
				_presentationSubmissions=value;
			}
		}
		#endregion

	}
}
