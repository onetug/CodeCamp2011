using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class EventAttendeePreferenceValue
    {
        partial void Question_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.PreferenceValue.Preference.Question;

        }

        partial void Answer_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.PreferenceValue.Answer;

        }

        partial void AttendeeName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.EventAttendee.Person.Name;

        }
    }
}
