using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class PreferenceValue
    {
        partial void Question_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Preference.Question;

        }
    }
}
