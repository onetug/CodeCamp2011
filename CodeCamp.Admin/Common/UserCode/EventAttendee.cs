using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class EventAttendee
    {
        partial void AttendeeName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Person.Name;

        }

        partial void EventName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = GroupEvent.Name;

        }
    }
}
