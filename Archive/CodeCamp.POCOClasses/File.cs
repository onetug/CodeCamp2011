using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class File
	{
		public File()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _description;
		private String _type;
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
		public virtual String Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type=value;
			}
		}
		#endregion

	}
}
