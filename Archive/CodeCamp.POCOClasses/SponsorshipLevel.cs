using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class SponsorshipLevel
	{
		public SponsorshipLevel()
		{
		}

		#region fields
		private Int32 _id;
		private String _name;
		private String _description;
		private String _amount;
		private ICollection<SponsorshipLevel> _sponsors;
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
		public virtual String Amount
		{
			get
			{
				return _amount;
			}
			set
			{
				_amount=value;
			}
		}
		public virtual ICollection<SponsorshipLevel> Sponsors
		{
			get
			{
				return _sponsors;
			}
			set
			{
				_sponsors=value;
			}
		}
		#endregion

	}
}
