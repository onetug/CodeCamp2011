using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class TrackOwner
    {
        partial void TrackName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Track.Name;

        }

        partial void TrackOwnerName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Person.Name;

        }
    }
}
