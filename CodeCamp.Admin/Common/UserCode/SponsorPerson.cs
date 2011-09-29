using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class SponsorPerson
    {
        partial void SponsorContactName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Sponsor.SponsorPersons.FirstOrDefault().Person.Name;

        }

        partial void SponsorName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Sponsor.Name;

        }
    }
}
