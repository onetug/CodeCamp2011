using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Room
	{
		public Room()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _description;
		private Int32 _maxCapacity;
		private ICollection<Room> _locations;
		private ICollection<Room> _sessions;
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
		public virtual ICollection<Room> Locations
		{
			get
			{
				return _locations;
			}
			set
			{
				_locations=value;
			}
		}
		public virtual ICollection<Room> Sessions
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
