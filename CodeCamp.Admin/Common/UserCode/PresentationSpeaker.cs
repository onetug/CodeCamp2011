using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class PresentationSpeaker
    {
        partial void SpeakerName_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Person.Name;

        }

        partial void PresentationTitle_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Presentation.Name;

        }

        partial void PresentationDescription_Compute(ref string result)
        {
            // Set result to the desired field value
            result = this.Presentation.Description;

        }
    }
}
