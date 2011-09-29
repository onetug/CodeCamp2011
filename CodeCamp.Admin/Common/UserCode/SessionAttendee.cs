using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class SessionAttendee
    {
        partial void SessionTitle_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Session.Name;

        }

        partial void AttendeeName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.EventAttendee.Person.Name;

        }
    }
}
