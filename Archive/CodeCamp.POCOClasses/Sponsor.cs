using System;
using System.Collections.Generic;

namespace CodeCamp.CoreClasses
{
	public partial class Sponsor
	{
		public Sponsor()
		{
		}

		#region fields
		private Int32 _id;
		private ICollection<Sponsor> _events;
		private String _name;
		private String _description;
		private ICollection<Person> _owners;
		private String _approvalStatus;
		private ICollection<SponsorshipLevel> _sponsorshipLevels;
		private String _url;
		private String _image;
		private String _notes;
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
		public virtual ICollection<Sponsor> Events
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
		public virtual ICollection<Person> Owners
		{
			get
			{
				return _owners;
			}
			set
			{
				_owners=value;
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
		public virtual ICollection<SponsorshipLevel> SponsorshipLevels
		{
			get
			{
				return _sponsorshipLevels;
			}
			set
			{
				_sponsorshipLevels=value;
			}
		}
		public virtual String Url
		{
			get
			{
				return _url;
			}
			set
			{
				_url=value;
			}
		}
		public virtual String Image
		{
			get
			{
				return _image;
			}
			set
			{
				_image=value;
			}
		}
		public virtual String Notes
		{
			get
			{
				return _notes;
			}
			set
			{
				_notes=value;
			}
		}
		#endregion

	}
}
