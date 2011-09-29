using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Location
	{
		public Location()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _address1;
		private String _address2;
		private String _city;
		private String _state;
		private String _zip;
		private ICollection<Location> _events;
		private ICollection<Room> _rooms;
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
		public virtual String Address1
		{
			get
			{
				return _address1;
			}
			set
			{
				_address1=value;
			}
		}
		public virtual String Address2
		{
			get
			{
				return _address2;
			}
			set
			{
				_address2=value;
			}
		}
		public virtual String City
		{
			get
			{
				return _city;
			}
			set
			{
				_city=value;
			}
		}
		public virtual String State
		{
			get
			{
				return _state;
			}
			set
			{
				_state=value;
			}
		}
		public virtual String Zip
		{
			get
			{
				return _zip;
			}
			set
			{
				_zip=value;
			}
		}
		public virtual ICollection<Location> Events
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
		#endregion

	}
}
